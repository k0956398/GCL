namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// The result of an E+E protocol command
    /// </summary>
    public interface IEECommandResult
    {
        EECmdResultCode Code { get; }

        byte[] Data { get; }
    }
}