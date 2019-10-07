namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetModeConfCmdResult : EECmdResultBase
    {
        /// <summary>
        /// The EE31 protocol data type.
        /// </summary>
        public byte DataType { get; private set; }

        /// <summary>
        /// Index of mode to configure.
        /// 0-127: product specific
        /// 128-255: standardized (see EE31 protocol reference)
        /// </summary>
        public byte ModeIdx { get; private set; }

        /// <summary>
        /// Converted (and possibly scaled) value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Value raw data.
        /// </summary>
        public byte[] ValueData { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            if (Data.Length < 4)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            ModeIdx = Data[1];
            DataType = Data[2];
            if (EE31DataType.ToValue(DataType, Data, 3, out object value, out byte[] valueData))
            {
                ValueData = valueData;
                Value = value;
            }
            else
                Code = EECmdResultCode.InvalidResult;
        }
    }
}