namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class SwitchBootloaderCmdResult : EECmdResultBase
    {
        public bool KeepUsingBusAddr { get; private set; }

        public bool SetTo96008N1 { get; private set; }

        public ushort WaitForMs { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 4)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                // Create command specific result from data
                SetTo96008N1 = Data[0] != 0;
                KeepUsingBusAddr = Data[1] != 0;
                WaitForMs = DataTypeConverter.ByteConverter.ToUInt16(Data, 2, reverseByteOrder);
            }
        }
    }
}