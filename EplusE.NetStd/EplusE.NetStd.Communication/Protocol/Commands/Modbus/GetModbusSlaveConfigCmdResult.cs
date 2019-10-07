namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetModbusSlaveConfigCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Datatype
        /// </summary>
        public byte DataType { get; private set; }

        /// <summary>
        /// Datapoint index linked to this modbus address
        /// </summary>
        public byte DpIdx { get; private set; }

        /// <summary>
        /// Scale factor
        /// </summary>
        public ushort ScaleFactor { get; private set; }

        /// <summary>
        /// The datapoint variant
        /// </summary>
        public ValueVariant Variant { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 6)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            DpIdx = Data[1];
            Variant = (ValueVariant)Data[2];
            DataType = Data[3];
            ScaleFactor = DataTypeConverter.ByteConverter.ToUInt16(Data, 4, reverseByteOrder);
        }
    }
}