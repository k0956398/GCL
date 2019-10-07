namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command result base class that implements IEECommandResult.
    /// </summary>
    public class EECmdResultBase : IEECommandResult
    {
        public EECmdResultBase()
        {
        }

        public EECmdResultCode Code { get; internal set; }

        public byte[] Data { get; internal set; }

        /// <summary>
        /// Result classes might overwrite to provide special result handling.
        /// </summary>
        internal virtual void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
        }
    }
}