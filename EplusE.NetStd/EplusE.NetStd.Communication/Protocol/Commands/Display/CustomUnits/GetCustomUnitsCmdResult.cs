namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetCustomUnitsCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Custom unit string
        /// </summary>
        public string Unit { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 1)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Unit = StringHelper.ExtractStringContent(Data, 1, -1);
        }
    }
}