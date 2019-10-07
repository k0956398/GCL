namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetWirelessMaxIdleTimeCmdParams : EECmdParamBase
    {
        public GetWirelessMaxIdleTimeCmdParams() :
            base(0x0)
        {
            //... set bytes
        }
    }
}