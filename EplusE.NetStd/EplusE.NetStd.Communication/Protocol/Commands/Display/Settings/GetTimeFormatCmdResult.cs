namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetTimeFormatCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Time format (0 = hh:mm:ss, 1 = hh:mm)
        /// </summary>
        public byte TimeFormat { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 2)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            TimeFormat = Data[1];
        }
    }
}