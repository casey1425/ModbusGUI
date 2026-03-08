using System;
using System.Collections.Generic;
using System.Text;

namespace ModbusGUI.Models
{
    public class ModbusResult
    {
        public bool IsSuccess { get; set; }
        public byte[] Data { get; set; }
        public string ErrorMessage { get; set; }
        public static ModbusResult Success (byte[] data)
            => new ModbusResult { IsSuccess = true, Data = data };
        public static ModbusResult Fail (string message)
            => new ModbusResult { IsSuccess = false, ErrorMessage = message };
    }
}
