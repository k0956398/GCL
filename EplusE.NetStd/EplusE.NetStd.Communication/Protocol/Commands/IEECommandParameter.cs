namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// The parameter required to execute an E+E protocol command
    /// </summary>
    public interface IEECommandParameter
    {
        byte Cmd { get; }
        ushort? CmdBusAddr { get; }
        byte[] CmdData { get; }
        uint? CmdTimeoutMs { get; }
        uint? CmdTries { get; }
    }
}