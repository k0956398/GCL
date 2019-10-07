using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetModeConfCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Set mode configuration data.
        /// </summary>
        /// <param name="modeIdx">Index of mode to get configuration data.
        /// 0-127: product specific
        /// 128-255: standardized (see EE31 protocol reference)
        /// </param>
        /// <param name="dataType">The EE31 Protocol data type.</param>
        /// <param name="valueData">Data to set.</param>
        public SetModeConfCmdParams(byte modeIdx, byte dataType, byte[] valueData) :
            base(0x0)
        {
            //... set bytes
        }

        /// <summary>
        /// Set mode configuration data for common used type (data type = 7, byte)
        /// </summary>
        /// <param name="modeIdx">Index of mode to get configuration data.
        /// 0-127: product specific
        /// 128-255: standardized (see EE31 protocol reference)
        /// </param>
        /// <param name="dataType">The EE31 Protocol data type.</param>
        /// <param name="valueData">Data to set.</param>
        public SetModeConfCmdParams(byte modeIdx, byte value) :
            base(0x0)
        {
            //... set bytes
        }
    }
}