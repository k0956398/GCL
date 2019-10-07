namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOutputConfigCmdResult_Analog : GetOutputConfigCmdResultBase
    {
        internal GetOutputConfigCmdResult_Analog(OutputMode mode) : base(mode)
        { }

        public double MVRangeMax { get; private set; }

        public double MVRangeMin { get; private set; }

        public double PhysRangeMax { get; private set; }

        public double PhysRangeMin { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            base.InterpretResult(reverseByteOrder, cmdConv, cmdParams);

            // Create command specific result from data
            if (Code == EECmdResultCode.Success)
            {
                if (Data.Length < 20)
                {
                    Code = EECmdResultCode.InvalidResult;
                    return;
                }

                // ACK received, response data valid
                int dataIdx = 3;
                MVCode = cmdConv.MVIndexToMVCode(Data[dataIdx]);
                dataIdx++;
                MVRangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                dataIdx += 4;
                MVRangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                dataIdx += 4;
                PhysRangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                dataIdx += 4;
                PhysRangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                dataIdx += 4;
                // OPTIONAL: Variant Index (Byte)
                Variant = OptionalVariant(Data, ref dataIdx);
                // OPTIONAL: Error Indication
                ErrorIndicationEnabled = OptionalErrorIndication(ref dataIdx, reverseByteOrder, out double? value);
                ErrorIndicationValue = value;
            }
        }

        private bool? OptionalErrorIndication(ref int dataIdx, bool isReverseByteOrder, out double? errorIndicationValue)
        {
            errorIndicationValue = null;
            if ((dataIdx + 1) > Data.Length)
                return null;

            // Device has error indication
            // (it still may be disabled if there is no further data).
            if (Data[dataIdx++] == 1)
            {
                // Return error indication value if found
                if ((dataIdx + 4) <= Data.Length)
                {
                    float value = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, isReverseByteOrder);
                    dataIdx += 4;

                    errorIndicationValue = value.ToDoubleWithFloatResolution();
                    return true;
                }
            }

            return false;
        }
    }
}