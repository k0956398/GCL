using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOut1Out2MVAndRangeRangeCmdResult : EECmdResultBase
    {
        public MVCode Out1_MVCode { get; private set; }

        public float Out1_MVRangeMax { get; private set; }

        public float Out1_MVRangeMin { get; private set; }

        public MVCode Out2_MVCode { get; private set; }

        public float Out2_MVRangeMax { get; private set; }

        public float Out2_MVRangeMin { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 18)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Out1_MVCode = cmdConv.MVIndexToMVCode(Data[0]);
            Out2_MVCode = cmdConv.MVIndexToMVCode(Data[1]);

            Out1_MVRangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, 2, reverseByteOrder);
            Out1_MVRangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, 6, reverseByteOrder);

            Out2_MVRangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, 10, reverseByteOrder);
            Out2_MVRangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, 14, reverseByteOrder);
        }
    }
}