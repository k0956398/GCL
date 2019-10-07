namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetFirmwareRevCmdParams : EECmdParamBase
    {
        public GetFirmwareRevCmdParams(bool fromSlave = false, bool factoryData = false) :
            base(0x0, 0)
        {
            //... set bytes
        }
    }
}