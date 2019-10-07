namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetStatusBarVisCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Visibility (0 = invisible, 1 = always visible, 2 = dynamic)
        /// </summary>
        public byte Visibility { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 2)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Visibility = Data[1];
        }
    }
}