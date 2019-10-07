namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command parameter base class that implements IEECommandParameter.
    /// </summary>
    public class EECmdParamBase : IEECommandParameter
    {
        internal EECmdParamBase(byte cmd, byte[] param = null)
        {
            Cmd = cmd;
            CmdData = param;
        }

        internal EECmdParamBase(byte cmd, uint cmdTries, byte[] param = null)
        {
            Cmd = cmd;
            CmdTries = cmdTries;
            CmdData = param;
        }

        internal EECmdParamBase(byte cmd, uint cmdTries, uint cmdTimeoutMs, byte[] param = null)
        {
            Cmd = cmd;
            CmdTries = cmdTries;
            CmdTimeoutMs = cmdTimeoutMs;
            CmdData = param;
        }

        internal EECmdParamBase(byte cmd, ushort cmdAddr, uint cmdTries, uint cmdTimeoutMs, byte[] param = null)
        {
            Cmd = cmd;
            CmdBusAddr = cmdAddr;
            CmdTries = cmdTries;
            CmdTimeoutMs = cmdTimeoutMs;
            CmdData = param;
        }

        public byte Cmd { get; protected set; }
        public ushort? CmdBusAddr { get; protected set; }
        public byte[] CmdData { get; protected set; }
        public uint? CmdTimeoutMs { get; private set; }
        public uint? CmdTries { get; private set; }

        internal virtual void ConvertData(IEECmdConverters protConverters)
        {
        }
    }
}