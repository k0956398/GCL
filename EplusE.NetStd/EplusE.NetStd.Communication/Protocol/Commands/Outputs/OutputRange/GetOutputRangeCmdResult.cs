namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOutputRangeCmdResult : EECmdResultBase
    {
        public float RangeMax { get; private set; }

        public float RangeMin { get; private set; }

        public bool Voltage { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 9)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Voltage = Data[0] == 0x1;

            RangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, 1, reverseByteOrder);
            RangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, 5, reverseByteOrder);
        }
    }
}