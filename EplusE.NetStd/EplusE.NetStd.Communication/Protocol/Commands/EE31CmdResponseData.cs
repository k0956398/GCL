namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// EE31 command response data class.
    /// </summary>
    internal class EE31CmdResponseData
    {
        /// <summary>
        /// Gets or sets the found EE31 frame (if any).
        /// </summary>
        /// <value>The found EE31 frame (if any).</value>
        public byte[] FoundFrame { get; set; }

        /// <summary>
        /// Gets or sets the EE31 command response. @ACK: Payload data (without ACK), @NAK: Error
        /// code (1 Byte).
        /// </summary>
        /// <value>
        /// The EE31 command response. @ACK: Payload data (without ACK), @NAK: Error code (1 Byte).
        /// </value>
        public byte[] Response { get; set; }

        /// <summary>
        /// Gets or sets the EE31 command result.
        /// </summary>
        /// <value>The EE31 command result.</value>
        public EE31Result Result { get; set; }

        /// <summary>
        /// Gets or sets the received bus address of device.
        /// </summary>
        /// <value>The received bus address of device.</value>
        public int RxBusAddr { get; set; }
    }
}