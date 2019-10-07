namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetSerialCmdResult : EECmdResultBase
    {
        public string Serial { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            Serial = StringHelper.ExtractStringContent(Data);

            // Validate data
            if (string.IsNullOrWhiteSpace(Serial))
                Code = EECmdResultCode.InvalidResult;
        }
    }
}