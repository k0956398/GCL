namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetWirelessMACCmdParams : EECmdParamBase
    {
        public GetWirelessMACCmdParams() :
            base(0x0)
        {
            //... set bytes
        }
    }
}