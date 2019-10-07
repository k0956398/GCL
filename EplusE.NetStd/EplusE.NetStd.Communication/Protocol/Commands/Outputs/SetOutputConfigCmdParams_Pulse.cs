using EplusE.Measurement;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputConfigCmdParams_Pulse : SetOutputConfigCmdParamsBase
    {
        /// <summary>
        /// Pulse output parameter construction
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="normallyInverted"></param>
        /// <param name="mvCode"></param>
        /// <param name="mvRangeMin"></param>
        /// <param name="mvRangeMax"></param>
        /// <param name="pulseWidth"></param>
        /// <param name="pulseWeight"></param>
        /// <param name="mvVariant"></param>
        /// <param name="errorIndicationEnabled"></param>
        /// <param name="activeIfError"></param>
        public SetOutputConfigCmdParams_Pulse(byte portId, bool normallyInverted, MVCode mvCode, float mvRangeMin, float mvRangeMax,
            float pulseWidth, float pulseWeight, ValueVariant? mvVariant = null, bool? errorIndicationEnabled = null, bool activeIfError = false) :
            base(portId)
        {
            int size = 20;
            if (mvVariant != null || errorIndicationEnabled != null)
                size += 1; // additional byte for variant
            if (errorIndicationEnabled != null)
                size += errorIndicationEnabled.Value ? 2 : 1; // additional byte for enabled (2 additional if 'true')

            //... set bytes
        }
    }
}