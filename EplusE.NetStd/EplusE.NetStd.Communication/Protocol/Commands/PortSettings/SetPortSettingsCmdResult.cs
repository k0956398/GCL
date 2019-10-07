namespace EplusE.NetStd.Communication.Protocol.Commands
{
    public class SetPortSettingsCmdResult : EECmdResultBase
    {
        public bool SettingsSavedAndApplied { get { return !SettingsSavedNotApplied; } }

        public bool SettingsSavedNotApplied { get; private set; }

        internal override void InterpretResult(bool reverseByteOrder, IEECmdConverters cmdConv, IEECommandParameter cmdParams)
        {
            // Create command specific result from data
            if (Data.Length < 1)
                Code = EECmdResultCode.InvalidResult;
            else
            {
                SettingsSavedNotApplied = Data[0] == 0x1;
            }
        }
    }
}