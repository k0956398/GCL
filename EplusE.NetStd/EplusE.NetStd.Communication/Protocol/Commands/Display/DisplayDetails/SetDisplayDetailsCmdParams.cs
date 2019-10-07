namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDisplayDetailsCmdParams : EECmdParamBase
    {
        public SetDisplayDetailsCmdParams(int? switchInterval, int? backlightBrightness, int? Contrast, DisplayOrientation? displayOrientation) :
            base(0x0)
        {
            //... set bytes
        }
    }
}