using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ModbusGUI.Transports
{
    public class TcpTransport : IModbusTransport
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly string _ip;
        private readonly int _port;

        public bool IsConnected => _client != null && _client.Connected;

        public TcpTransport(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task ConnectAsync()
        {
            // 이미 연결되어 있다면 재연결 시도
            if (_client != null) Disconnect();

            _client = new TcpClient();
            // 타임아웃 설정
            _client.ReceiveTimeout = 3000;
            _client.SendTimeout = 3000;

            await _client.ConnectAsync(_ip, _port);
            _stream = _client.GetStream();
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
            _stream = null;
            _client = null;
        }

        public async Task<byte[]> SendAndReceiveAsync(byte[] request)
        {
            if (!IsConnected) throw new Exception("연결되지 않았습니다.");

            // 1. 전송
            await _stream.WriteAsync(request, 0, request.Length);

            // 2. 수신: MBAP Header 읽기
            byte[] header = new byte[6];
            int readBytes = await ReadExactAsync(header, 6);
            if (readBytes < 6) throw new Exception("응답 헤더 수신 실패");

            // 3. 남은 데이터 길이 계산
            // Big-Endian
            int bodyLength = (header[4] << 8) | header[5];

            if (bodyLength <= 0 || bodyLength > 256)
                throw new Exception($"유효하지 않은 데이터 길이: {bodyLength}");

            // 4. 나머지 데이터 읽기
            byte[] body = new byte[bodyLength];
            int bodyRead = await ReadExactAsync(body, bodyLength);
            if (bodyRead < bodyLength) throw new Exception("응답 데이터 수신 실패");

            // 5. 헤더와 바디를 합쳐서 전체 패킷 반환
            byte[] fullResponse = new byte[6 + bodyLength];
            Array.Copy(header, 0, fullResponse, 0, 6);
            Array.Copy(body, 0, fullResponse, 6, bodyLength);

            return fullResponse;
        }

        // 지정된 바이트 수만큼 확실히 읽어오는 헬퍼 메서드
        private async Task<int> ReadExactAsync(byte[] buffer, int length)
        {
            int totalRead = 0;
            while (totalRead < length)
            {
                int read = await _stream.ReadAsync(buffer, totalRead, length - totalRead);
                if (read == 0) break; // 연결 끊김
                totalRead += read;
            }
            return totalRead;
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}