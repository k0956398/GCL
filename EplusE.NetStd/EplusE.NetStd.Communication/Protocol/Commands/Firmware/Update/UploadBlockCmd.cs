namespace EplusE.NetStd.Communication.Protocol.Commands
{
    internal class UploadBlockCmd : EECmdBase<UploadBlockCmdParams, EECmdResultBase>
    {
        public override IEECommandResult Execute(IEECommandParameter parameter)
        {
            if (parameter is UploadBlockCmdParams uploadParams)
            {
                // Send command with payload (right after cmd CRC)
                EECmdResultCode result = ProtocolCallbacks.SendReceive(parameter, out byte[] response, uploadParams.BlockData);
                return CreateResult(result, response, parameter);
            }
            else
                return CreateResult(EECmdResultCode.InvalidParamClass, null, null);
        }
    }
}