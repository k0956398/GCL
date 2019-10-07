using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class DiscoveryAckCmd : EECmdBase<DiscoveryAckCmdParams, EECmdResultBase>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            if (parameter is DiscoveryAckCmdParams diasParams)
            {
                try
                {
                    ProtocolCallbacks.SendReceive(parameter, out byte[] response);
                }
                catch (Exception ex)
                {
                    // i.e. TimeoutException may occur, no problem
                    Diagnostic.Msg(1, "DiscoveryAckCmd", "Exception: " + ex.Message);
                }

                // Always return success because transmitters will not answer on this command
                return CreateResult(EECmdResultCode.Success, null, parameter);
            }
            else
                return CreateResult(EECmdResultCode.InvalidParamClass, null, null);
        }
    }
}