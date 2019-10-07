namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command interface to execute E+E protocol commands
    /// </summary>
    internal interface IEECommCommand
    {
        IEECmdCallbacks ProtocolCallbacks { get; set; }

        IEECmdConverters ProtocolConverters { get; set; }

        IEECommandResult Execute(IEECommandParameter parameter);
    }
}