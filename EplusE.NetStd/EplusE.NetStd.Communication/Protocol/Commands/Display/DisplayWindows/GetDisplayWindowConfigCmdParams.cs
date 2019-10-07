namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetDisplayWindowConfigCmdParams : EECmdParamBase
    {
        public GetDisplayWindowConfigCmdParams(byte wndNr) :
            base(0x0)
        {
            //... set bytes
        }
    }
}