namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDisplayWindowSlotConfigCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Linked datapoint index to display
        /// </summary>
        public byte DpIdx { get; private set; }

        /// <summary>
        /// Displayed name of datapoint
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Precistion of the displayed value
        /// </summary>
        public byte Precision { get; private set; }

        /// <summary>
        /// The datapoint variant
        /// </summary>
        public ValueVariant Variant { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 4)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            DpIdx = Data[1];
            Variant = (ValueVariant)Data[2];
            Precision = Data[3];
            Name = StringHelper.ExtractStringContent(Data, 4, -1);
        }
    }
}