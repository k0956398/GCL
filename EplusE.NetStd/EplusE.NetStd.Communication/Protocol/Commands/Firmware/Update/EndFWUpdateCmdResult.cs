namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class EndFWUpdateCmdResult : EECmdResultBase
    {
        public ushort WaitForBootMs { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 2)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // Create command specific result from data
                WaitForBootMs = DataTypeConverter.ByteConverter.ToUInt16(Data, 0, reverseByteOrder);
            }
        }
    }
}