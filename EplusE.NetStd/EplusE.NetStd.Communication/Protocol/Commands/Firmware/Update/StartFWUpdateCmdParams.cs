namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class StartFWUpdateCmdParams : EECmdParamBase
    {
        public StartFWUpdateCmdParams(ushort hwCode) :
            base(0x0, 0, 0)
        {
            //... set bytes
        }
    }
}