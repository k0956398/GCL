namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetModbusSlaveConfigCmdParams : EECmdParamBase
    {
        public SetModbusSlaveConfigCmdParams(byte modbusAddrOffset, byte dpIdx, ValueVariant dataVariant, byte dataType, ushort scaleFactor) :
            base(0x0)
        {
            //... set bytes
        }
    }
}