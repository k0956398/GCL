namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDateFormatCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Date format (0 = dd.mm.yy, 1 = mm.dd.yy, 2 = yyyy-mm-dd)
        /// </summary>
        public byte DateFormat { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 2)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            DateFormat = Data[1];
        }
    }
}