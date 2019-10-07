namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Possible EE31 protocol result codes (ACK, NAK, etc).
    /// </summary>
    internal enum EE31Result : byte
    {
        /// <summary>
        /// Invalid
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Acknowlegde
        /// </summary>
        ACK = 0x06,

        /// <summary>
        /// Negative Acknowlegde
        /// </summary>
        NAK = 0x15
    };
}