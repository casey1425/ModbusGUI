using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace ModbusGUI.Transports
{
    public class SerialTransport : IModbusTransport
    {
        private SerialPort _serialPort;

        public bool IsConnected => _serialPort != null && _serialPort.IsOpen;
        public SerialTransport(string portName, int baudRate)
        {
            _serialPort = new SerialPort(portName, baudRate, Parity.Even, 8, StopBits.One);
            _serialPort.ReadTimeout = 1000;
            _serialPort.WriteTimeout = 1000;
        }

        public async Task ConnectAsync()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                await Task.Delay(100);
            }
        }

        public void Disconnect()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public async Task<byte[]> SendAndReceiveAsync(byte[] requestPacket)
        {
            if (!IsConnected) throw new Exception("Serial Port Not Open");

            // 1. 요청 전송
            // 시리얼 버퍼 비우기
            _serialPort.DiscardInBuffer();
            _serialPort.Write(requestPacket, 0, requestPacket.Length);

            // 2. 응답 수신
            await Task.Delay(500);

            int bytesToRead = _serialPort.BytesToRead;
            if (bytesToRead == 0) throw new TimeoutException("응답이 없습니다 (Timeout).");

            byte[] buffer = new byte[bytesToRead];
            _serialPort.Read(buffer, 0, bytesToRead);

            return buffer;
        }

        public void Dispose()
        {
            Disconnect();
            _serialPort?.Dispose();
        }
    }
}