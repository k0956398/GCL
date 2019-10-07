namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class HardwareComponent
    {
        /// <summary>
        /// Attributes (optional, depends on "Type")
        /// Interface: port nr
        /// Output: output nr
        /// </summary>
        public byte[] Attributes { get; internal set; }

        /// <summary>
        /// Component category.
        /// Interface: 0 = RS485
        /// Output: 0 = analog, 1 = relay
        /// Display: 0
        /// </summary>
        public byte Category { get; internal set; }

        /// <summary>
        /// Hardware component type (0 = interface, 1 = output, 2 = display)
        /// </summary>
        public byte Component { get; internal set; }

        /// <summary>
        /// Text (optional)
        /// Output: terminal description (e.g.: "A1")
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Component type.
        /// Interface: 0 = modbus master, 1 = modbus slave
        /// Analog output: 0 = current, 1 = voltage, 2 = both
        /// Relay output: 0 = normally open, 1 = normally closed, 2 = changing
        /// Display: 0
        /// </summary>
        public byte Type { get; internal set; }
    }
}