using System.Threading;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class GetTunnelCmd : EECmdBase<GetTunnelCmdParams, GetTunnelCmdResult>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            IEECommandResult commandResult = base.Execute(parameter);

            // BUSY retries if configured
            int retries = (parameter as GetTunnelCmdParams).TunnelCmdBusyRetries;
            while (commandResult.Code == EECmdResultCode.Busy && retries-- > 0)
            {
                Thread.Sleep((parameter as GetTunnelCmdParams).TunnelCmdBusyWaitMs);
                commandResult = base.Execute(parameter);

                Diagnostic.Msg(4, "GetTunnelCmd", "Retry after BUSY");
            }

            return commandResult;
        }
    }
}