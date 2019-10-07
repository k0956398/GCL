namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class EndFWUpdateCmdParams : EECmdParamBase
    {
        public EndFWUpdateCmdParams() :
            base(0x0, 0, 0, null)
        {
        }
    }
}