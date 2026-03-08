using System;
using System.IO;
using System.Threading.Tasks;
using ModbusGUI.Models;
using ModbusGUI.Transports;

namespace ModbusGUI.Protocol
{
    public class ModbusClient
    {
        private readonly IModbusTransport _transport;
        private ushort _transactionId = 0;

        public ModbusClient(IModbusTransport transport)
        {
            _transport = transport;
        }

        // ==========================================
        // 1. FC01: Read Coils
        // ==========================================
        public async Task<ModbusResult> ReadCoilsAsync(byte unitId, ushort startAddr, ushort count)
        {
            return await PerformReadAsync(unitId, 0x01, startAddr, count);
        }

        // ==========================================
        // 2. FC02: Read Discrete Inputs 
        // ==========================================
        public async Task<ModbusResult> ReadDiscreteInputsAsync(byte unitId, ushort startAddr, ushort count)
        {
            return await PerformReadAsync(unitId, 0x02, startAddr, count);
        }

        // ==========================================
        // 3. FC03: Read Holding Registers
        // ==========================================
        public async Task<ModbusResult> ReadHoldingRegistersAsync(byte unitId, ushort startAddr, ushort count)
        {
            return await PerformReadAsync(unitId, 0x03, startAddr, count);
        }

        // ==========================================
        // 4. FC04: Read Input Registers
        // ==========================================
        public async Task<ModbusResult> ReadInputRegistersAsync(byte unitId, ushort startAddr, ushort count)
        {
            return await PerformReadAsync(unitId, 0x04, startAddr, count);
        }

        // ==========================================
        // [공통 내부 함수] 모든 읽기 명령을 처리
        // ==========================================
        private async Task<ModbusResult> PerformReadAsync(byte unitId, byte fc, ushort startAddr, ushort count)
        {
            try
            {
                byte[] request;

                // Transport 종류에 따라 패킷 생성 (TCP / RTU)
                if (_transport is TcpTransport)
                {
                    request = BuildTcpPacket(unitId, fc, startAddr, count);
                }
                else // SerialTransport
                {
                    request = BuildRtuPacket(unitId, fc, startAddr, count);
                }

                // 전송 및 수신
                byte[] response = await _transport.SendAndReceiveAsync(request);

                // 응답 검증
                int fcIndex = (_transport is TcpTransport) ? 7 : 1;

                if (response.Length <= fcIndex) return ModbusResult.Fail("응답 데이터가 너무 짧습니다.");

                // 에러 코드 체크 (Function Code + 0x80)
                if (response[fcIndex] >= 0x80)
                {
                    return ModbusResult.Fail($"Modbus Exception: Code {response[fcIndex + 1]}");
                }

                // 데이터 추출
                int byteCountIndex = fcIndex + 1;
                int dataIndex = fcIndex + 2;
                int byteCount = response[byteCountIndex];

                // 실제 데이터 길이 확인
                if (response.Length < dataIndex + byteCount)
                {
                    return ModbusResult.Fail("응답 데이터 길이가 부족합니다.");
                }

                byte[] data = new byte[byteCount];
                Array.Copy(response, dataIndex, data, 0, byteCount);

                return ModbusResult.Success(data);
            }
            catch (Exception ex)
            {
                return ModbusResult.Fail(ex.Message);
            }
        }

        // TCP 패킷 빌더
        private byte[] BuildTcpPacket(byte unitId, byte fc, ushort addr, ushort count)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(ToBigEndian(++_transactionId));
                bw.Write((ushort)0);
                bw.Write(ToBigEndian((ushort)6)); // Length
                bw.Write(unitId);
                bw.Write(fc);
                bw.Write(ToBigEndian(addr));
                bw.Write(ToBigEndian(count));
                return ms.ToArray();
            }
        }

        // RTU 패킷 빌더
        private byte[] BuildRtuPacket(byte slaveId, byte fc, ushort addr, ushort count)
        {
            // 1. 데이터 부분 생성
            byte[] data;
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(slaveId);
                bw.Write(fc);
                bw.Write(ToBigEndian(addr));
                bw.Write(ToBigEndian(count));
                data = ms.ToArray();
            }

            // 2. CRC 계산 및 추가
            byte[] crc = Crc16.Compute(data);

            // 3. 합치기
            byte[] fullPacket = new byte[data.Length + crc.Length];
            Array.Copy(data, 0, fullPacket, 0, data.Length);
            Array.Copy(crc, 0, fullPacket, data.Length, crc.Length);

            return fullPacket;
        }

        // Big Endian 변환 헬퍼
        private ushort ToBigEndian(ushort value) => (ushort)((value << 8) | (value >> 8));
    }
}