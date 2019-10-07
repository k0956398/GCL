namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOut1Out2RangeCmdResult : EECmdResultBase
    {
        public float Out1_RangeMax { get; private set; }

        public float Out1_RangeMin { get; private set; }

        public bool Out1_Voltage { get; private set; }

        public float Out2_RangeMax { get; private set; }

        public float Out2_RangeMin { get; private set; }

        public bool Out2_Voltage { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 18)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Out1_Voltage = Data[0] == 0x1;
            Out2_Voltage = Data[1] == 0x1;

            Out1_RangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, 2, reverseByteOrder);
            Out1_RangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, 6, reverseByteOrder);

            Out2_RangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, 10, reverseByteOrder);
            Out2_RangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, 14, reverseByteOrder);
        }
    }
}