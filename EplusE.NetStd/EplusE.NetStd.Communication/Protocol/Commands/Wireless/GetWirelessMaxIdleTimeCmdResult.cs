namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetWirelessMaxIdleTimeCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Wireless interface disabled after X seconds (0 = permanent on)
        /// </summary>
        public ushort MaxIdleTimeSec { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 3)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            MaxIdleTimeSec = DataTypeConverter.ByteConverter.ToUInt16(Data, 1, reverseByteOrder);
        }
    }
}