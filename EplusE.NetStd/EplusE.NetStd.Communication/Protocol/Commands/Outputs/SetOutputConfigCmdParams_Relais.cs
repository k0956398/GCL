using EplusE.Measurement;
using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputConfigCmdParams_Relais : SetOutputConfigCmdParamsBase
    {
        /// <summary>
        /// Relais output parameter construction (hysteresis or window mode)
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="normallyInverted"></param>
        /// <param name="mvCode"></param>
        /// <param name="mvRangeMin"></param>
        /// <param name="mvRangeMax"></param>
        /// <param name="switchPoint1"></param>
        /// <param name="hysteresis1"></param>
        /// <param name="switchPoint2"></param>
        /// <param name="hysteresis2"></param>
        /// <param name="mvVariant"></param>
        /// <param name="errorIndicationEnabled"></param>
        /// <param name="activeIfError"></param>
        public SetOutputConfigCmdParams_Relais(byte portId, bool normallyInverted, MVCode mvCode, float mvRangeMin, float mvRangeMax,
            float switchPoint1, float hysteresis1, float? switchPoint2, float? hysteresis2,
            ValueVariant? mvVariant = null, bool? errorIndicationEnabled = null, bool activeIfError = false) :
            base(portId)
        {
            //... set bytes
        }

        /// <summary>
        /// Relais output parameter construction (error indication mode)
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="normallyInverted"></param>
        /// <param name="errorIndicationEnabled"></param>
        /// <param name="activeIfError"></param>
        public SetOutputConfigCmdParams_Relais(byte portId, bool normallyInverted, bool errorIndicationEnabled, bool activeIfError = false) :
            base(portId)
        {
            //... set bytes
        }
    }
}