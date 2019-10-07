namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetUserTextCmdResult : EECmdResultBase
    {
        public string Text { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // First byte is string length
                int strLen = Data[0];
                Text = StringHelper.ExtractStringContent(Data, 1, strLen);
            }
        }
    }
}