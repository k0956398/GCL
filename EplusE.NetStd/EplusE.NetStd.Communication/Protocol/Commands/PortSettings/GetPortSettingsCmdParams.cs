namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetPortSettingsCmdParams : EECmdParamBase
    {
        public GetPortSettingsCmdParams(byte? portNr = null) :
            base(0x0, 0)
        {
            //... set bytes
        }
    }
}