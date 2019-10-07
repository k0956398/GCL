namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class GetSerialCmd : EECmdBase<GetSerialCmdParams, GetSerialCmdResult>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            IEECommandResult commandResult = base.Execute(parameter);
            if (commandResult.Code != EECmdResultCode.Success)
                return commandResult;

            if (commandResult.Code == EECmdResultCode.InvalidResult && !(parameter as GetSerialCmdParams).FactoryData)
            {
                // Maybe a device that does not support the fallback scenario
                // (customer sernr is empty, deliver factory sernr)
                commandResult = base.Execute(new GetSerialCmdParams(false, true));
            }

            return commandResult;
        }
    }
}