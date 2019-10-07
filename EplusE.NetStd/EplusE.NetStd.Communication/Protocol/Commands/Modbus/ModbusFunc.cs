using System.Xml.Serialization;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public enum ModbusFunc : byte
    {
        /// <summary>
        /// Read modbus holding register
        /// </summary>
        [XmlEnum(Name = "Read_HoldingRegister")]
        Read_HoldingRegister = 3,

        /// <summary>
        /// Read modbus input register
        /// </summary>
        [XmlEnum(Name = "Read_InputRegister")]
        Read_InputRegister = 4
    }
}