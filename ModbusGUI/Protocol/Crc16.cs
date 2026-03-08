using System;
using System.Collections.Generic;
using System.Text;

namespace ModbusGUI.Protocol
{
    public static class Crc16
    {
        public static byte[] Compute(byte[] data)
        {
            ushort crc = 0xFFFF;

            for(int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) == 1)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc >>= 1;
                }
            }
            return new byte[] { (byte)(crc & 0xFF), (byte)(crc >> 8)};
        }
    }
}
