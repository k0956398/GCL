namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDataPointConfigCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Index of custom unit (custom units table)
        /// </summary>
        public byte CustomUnitIdx { get; private set; }

        /// <summary>
        /// Datatype
        /// </summary>
        public byte DataType { get; private set; }

        /// <summary>
        /// Datapoint function code
        /// </summary>
        public ModbusFunc FuncCode { get; private set; }

        /// <summary>
        /// Upper limit to indicate error
        /// </summary>
        public float LimitErrorMax { get; private set; }

        /// <summary>
        /// Lower limit to indicate error
        /// </summary>
        public float LimitErrorMin { get; private set; }

        /// <summary>
        /// Hysteresis limit
        /// </summary>
        public float LimitHysteresis { get; private set; }

        /// <summary>
        /// Upper limit to indicate warning
        /// </summary>
        public float LimitWarningMax { get; private set; }

        /// <summary>
        /// Lower limit to indicate warning
        /// </summary>
        public float LimitWarningMin { get; private set; }

        /// <summary>
        /// ID (e.g. bus address) of probe where this datapoint belongs
        /// </summary>
        public byte ProbeID { get; private set; }

        /// <summary>
        /// Request interval in milliseconds
        /// </summary>
        public ushort RequestIntervalMs { get; private set; }

        /// <summary>
        /// Scale factor
        /// </summary>
        public ushort ScaleFactor { get; private set; }

        /// <summary>
        /// Register address of datapoint source
        /// </summary>
        public ushort SourceRegAddr { get; private set; }

        /// <summary>
        /// Modbus source of datapoint
        /// </summary>
        public ModbusSource SourceType { get; private set; }

        /// <summary>
        /// Unit of coordinated measurement values table (=254 if custom unit)
        /// </summary>
        public byte Unit { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 25)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            ProbeID = Data[1];
            SourceType = (ModbusSource)Data[2];
            FuncCode = (ModbusFunc)Data[3];
            SourceRegAddr = DataTypeConverter.ByteConverter.ToUInt16(Data, 4, reverseByteOrder);
            RequestIntervalMs = DataTypeConverter.ByteConverter.ToUInt16(Data, 6, reverseByteOrder);
            DataType = Data[8];
            ScaleFactor = DataTypeConverter.ByteConverter.ToUInt16(Data, 9, reverseByteOrder);
            // TODO: LimitErrorMin = DataTypeConverter.ByteConverter.ToFloat(Data, ??, reverseByteOrder);
            // TODO: LimitErrorMax = DataTypeConverter.ByteConverter.ToFloat(Data, ??, reverseByteOrder);
            LimitWarningMin = DataTypeConverter.ByteConverter.ToFloat(Data, 11, reverseByteOrder);
            LimitWarningMax = DataTypeConverter.ByteConverter.ToFloat(Data, 15, reverseByteOrder);
            LimitHysteresis = DataTypeConverter.ByteConverter.ToFloat(Data, 19, reverseByteOrder);
            CustomUnitIdx = Data[23];
            Unit = Data[24];
        }
    }
}