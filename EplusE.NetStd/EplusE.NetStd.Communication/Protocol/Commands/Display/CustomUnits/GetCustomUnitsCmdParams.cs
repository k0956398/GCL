namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetCustomUnitsCmdParams : EECmdParamBase
    {
        public GetCustomUnitsCmdParams(byte unitIdx) :
            base(0x0)
        {
            //... set bytes
        }
    }
}