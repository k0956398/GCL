namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class GetModeConfCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Get mode configuration data
        /// </summary>
        /// <param name="modeIdx">Index of mode to get configuration data.
        /// 0-127: product specific
        /// 128-255: standardized (see EE31 protocol reference)
        /// </param>
        public GetModeConfCmdParams(byte modeIdx) :
            base(0x0)
        {
            //... set bytes
        }
    }
}