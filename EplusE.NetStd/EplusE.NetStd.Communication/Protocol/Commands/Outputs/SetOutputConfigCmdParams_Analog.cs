using EplusE.Measurement;
using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class SetOutputConfigCmdParams_Analog : SetOutputConfigCmdParamsBase
    {
        /// <summary>
        /// Analog output parameter construction
        /// </summary>
        /// <param name="portId"></param>
        /// <param name="analogMode"></param>
        /// <param name="mvCode"></param>
        /// <param name="mvRangeMin"></param>
        /// <param name="mvRangeMax"></param>
        /// <param name="analogRangeMin"></param>
        /// <param name="analogRangeMax"></param>
        /// <param name="mvVariant"></param>
        /// <param name="errorIndicationEnabled"></param>
        /// <param name="errorIndication"></param>
        /// <param name="isInput"></param>
        public SetOutputConfigCmdParams_Analog(byte portId, byte analogMode, MVCode mvCode, float mvRangeMin, float mvRangeMax,
            float analogRangeMin, float analogRangeMax, ValueVariant? mvVariant = null, bool? errorIndicationEnabled = null, float errorIndication = 0, bool isInput = false) :
            base(portId, isInput)
        {
            if (analogMode != 0x0 && analogMode != 0x1)
                throw new ArgumentOutOfRangeException("analogMode", "Only 0x0 (current mode) or 0x1 (voltage mode) is allowed");

            //... set bytes
        }
    }
}