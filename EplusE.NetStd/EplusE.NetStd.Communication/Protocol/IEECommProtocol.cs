using EplusE.NetStd.Communication.Protocol.Commands;
using System;

namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// Represents the E+E Elektronik protocol layer. Requires IEECommDevice to handle the communication layer.
    /// </summary>
    public interface IEECommProtocol : IDisposable
    {
        IEECommandResult ExecuteCommand(EE31Command command, IEECommandParameter parameter);
    }
}