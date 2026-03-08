using System;
using System.Threading.Tasks;

namespace ModbusGUI.Transports
{
    public interface IModbusTransport : IDisposable
    {
        // 연결 상태 확인
        bool IsConnected { get; }

        // 연결 및 해제
        Task ConnectAsync();
        void Disconnect();

        // 요청을 보내고 응답을 받아오는 핵심 메서드
        Task<byte[]> SendAndReceiveAsync(byte[] requestPacket);
    }
}