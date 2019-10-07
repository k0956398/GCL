namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetDataPointConfigCmdParams : EECmdParamBase
    {
        public SetDataPointConfigCmdParams(byte dpIdx, byte probeID, ModbusSource sourceType, ModbusFunc funcCode, ushort sourceRegisterAddr,
                                           ushort requestIntervallMs, byte dataType, ushort scaleFactor,
                                           float limitErrorMin, float limitErrorMax,
                                           float limitWarningMin, float limitWarningMax, float limitHysteresis,
                                           byte customUnitIndex, byte unit) :
            base(0x0)
        {
            //... set bytes
        }
    }
}