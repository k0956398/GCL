namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class SetSerialCmdResult : EECmdResultBase
    {
        public int MaxLength { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // First byte is maximum string length
                MaxLength = Data[0];
            }
        }
    }
}