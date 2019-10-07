using System.Xml.Serialization;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public enum ModbusSource : byte
    {
        /// <summary>
        /// Source of datapoint is modbus master.
        /// Values are polled from attached modbus slaves.
        /// </summary>
        [XmlEnum(Name = "Modbus_Master")]
        Modbus_Master = 0,

        /// <summary>
        /// Source of datapoint is modbus slave.
        /// Values are updated by another modbus master.
        /// </summary>
        [XmlEnum(Name = "Modbus_Slave")]
        Modbus_Slave = 1
    }
}