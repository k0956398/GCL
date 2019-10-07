namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetModbusSlaveConfigCmdParams : EECmdParamBase
    {
        public GetModbusSlaveConfigCmdParams(byte modbusAddrOffset) :
            base(0x0)
        {
            //... set bytes
        }
    }
}