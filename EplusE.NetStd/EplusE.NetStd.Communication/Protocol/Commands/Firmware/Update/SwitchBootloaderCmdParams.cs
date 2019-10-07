namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SwitchBootloaderCmdParams : EECmdParamBase
    {
        public SwitchBootloaderCmdParams(ushort hwCode) :
            base(0x0, 0, 0)
        {
            //... set bytes
        }
    }
}