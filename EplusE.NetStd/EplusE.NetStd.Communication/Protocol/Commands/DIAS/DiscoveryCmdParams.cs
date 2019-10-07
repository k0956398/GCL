using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class DiscoveryCmdParams : EECmdParamBase
    {
        private static ushort _CurrentDiscoveryId = 0x7801;

        /// <summary>
        /// Constructor to create parameter for DIAS command with default timeout
        /// </summary>
        /// <param name="discoveryId">Unique random discovery id (use BuildNewDiscoveryId)</param>
        /// <param name="txBusAddr">Device bus address (0 for broadcast)</param>
        /// <param name="numberOfTimeslices">Number of 20ms time slices to delay responses</param>
        /// <param name="switchToProtocol">Switch to protocol (if neccessary), typ. EE31 protocol (0xB1)</param>
        /// <param name="timeoutMs">Command timeout in milliseconds (default: 500)</param>
        public DiscoveryCmdParams(ushort discoveryId, ushort txBusAddr = 0x0, byte numberOfTimeslices = 0x14, uint timeoutMs = 500, byte switchToProtocol = 0xB1) :
            base(0x00, txBusAddr, 0, timeoutMs)
        {
            //... set bytes
        }

        public static ushort BuildNewDiscoveryId(ushort step = 1)
        {
            ushort discoveryId = _CurrentDiscoveryId;

            // Advance to next discovery id number
            _CurrentDiscoveryId += step;

            // 0 is reserved/invalid value, skip this
            if (0 == _CurrentDiscoveryId)
                _CurrentDiscoveryId++;

            return discoveryId;
        }
    }
}