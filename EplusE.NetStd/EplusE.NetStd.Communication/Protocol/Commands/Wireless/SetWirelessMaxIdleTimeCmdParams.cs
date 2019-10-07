namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetWirelessMaxIdleTimeCmdParams : EECmdParamBase
    {
        public SetWirelessMaxIdleTimeCmdParams(ushort maxIdleTimeSec) :
            base(0x0)
        {
            //... set bytes
        }
    }
}