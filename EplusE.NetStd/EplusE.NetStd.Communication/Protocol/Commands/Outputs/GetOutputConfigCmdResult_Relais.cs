namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetOutputConfigCmdResult_Relais : GetOutputConfigCmdResultBase
    {
        internal GetOutputConfigCmdResult_Relais(OutputMode mode) : base(mode)
        {
        }

        public double Hysteresis1 { get; private set; }

        public double Hysteresis2 { get; private set; }

        public double MVRangeMax { get; private set; }

        public double MVRangeMin { get; private set; }

        public byte RelaisConfig { get; private set; }

        public double SwitchPoint1 { get; private set; }

        public double SwitchPoint2 { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            base.InterpretResult(reverseByteOrder, cmdConv, cmdParams);

            // Create command specific result from data
            if (Code == EECmdResultCode.Success)
            {
                // ACK received, response data valid
                int dataIdx = 2;
                RelaisConfig = Data[dataIdx++];
                if (OutputMode != OutputMode.Relais_ErrorIndication)
                {
                    if (Data.Length < 20)
                    {
                        Code = EECmdResultCode.InvalidResult;
                        return;
                    }

                    // Following data is not received in case of error indication
                    MVCode = cmdConv.MVIndexToMVCode(Data[dataIdx]);
                    dataIdx++;
                    MVRangeMin = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                    dataIdx += 4;
                    MVRangeMax = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                    dataIdx += 4;
                    SwitchPoint1 = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                    dataIdx += 4;
                    Hysteresis1 = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                    dataIdx += 4;
                    if (OutputMode == OutputMode.Relais_Window)
                    {
                        // 2nd point in window mode
                        SwitchPoint2 = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                        dataIdx += 4;
                        Hysteresis2 = DataTypeConverter.ByteConverter.ToFloat(Data, dataIdx, reverseByteOrder).ToDoubleWithFloatResolution();
                        dataIdx += 4;
                    }

                    // OPTIONAL: Variant Index (Byte)
                    Variant = OptionalVariant(Data, ref dataIdx);
                }
                else if (Data.Length < 4)
                {
                    Code = EECmdResultCode.InvalidResult;
                    return;
                }

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
                if ((dataIdx + 1) <= Data.Length)
                {
                    errorIndicationValue = Data[dataIdx++];
                    return true;
                }
            }

            return false;
        }
    }
}