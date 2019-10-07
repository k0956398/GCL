namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class GetDisplayWindowConfigCmdResult : EECmdResultBase
    {
        /// <summary>
        /// Display window enabled or disabled?
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Description (name) of the window
        /// </summary>
        public string Name { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // ACK received...
            if (Data.Length < 2)
            {
                Code = EECmdResultCode.InvalidResult;
                return;
            }

            Enabled = Data[1] != 0 ? true : false;

            Name = StringHelper.ExtractStringContent(Data, 2, -1);
        }
    }
}