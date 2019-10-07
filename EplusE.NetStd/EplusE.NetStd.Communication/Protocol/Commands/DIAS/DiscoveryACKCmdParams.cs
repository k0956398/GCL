using System;

namespace EplusE.NetStd.Communication.Protocol.Commands
{
    /// <summary>
    /// Command specific parameter class that extends EECmdParamBase
    /// </summary>
    public class DiscoveryAckCmdParams : EECmdParamBase
    {
        /// <summary>
        /// Constructor to create parameter for device discovery acknowledge command
        /// </summary>
        /// <param name="busAddr">Device bus address (must not be 0)</param>
        /// <param name="discoveryId">Device discovery id (same as used for 0x3E, 0x52 commands)</param>
        public DiscoveryAckCmdParams(ushort busAddr, ushort discoveryId) :
            base(0x00, busAddr, 0, 0)
        {
            if (busAddr == 0)
                throw new ArgumentException("Parameter must not be 0", "busAddr");

            //... set bytes
        }
    }
}