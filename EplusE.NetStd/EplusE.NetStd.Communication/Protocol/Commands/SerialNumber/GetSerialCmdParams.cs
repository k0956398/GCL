namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetSerialCmdParams : EECmdParamBase
    {
        public GetSerialCmdParams(bool fromSlave = false, bool factoryData = false) :
            base(0x0, 0)
        {
            //... set bytes
        }

        public bool FactoryData { get; private set; }
        public bool FromSlave { get; private set; }
    }
}