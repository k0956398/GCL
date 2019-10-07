using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication.Protocol
{
    /// <summary>
    /// Factory to create protocol instances and return IEECommProtocol interface
    /// </summary>
    public static class EECommProtocolFactory
    {
        private readonly static object _protectInstances = new object();

        private static List<EE31Protocol> _protInstances = new List<EE31Protocol>();

        /// <summary>
        /// Create instance of EE31 protocol class.
        /// </summary>
        /// <param name="communicationInterface">The communication layer</param>
        /// <returns></returns>
        public static IEECommProtocol GetEE31Protocol(IEECommDevice communicationInterface)
        {
            if (communicationInterface == null)
                throw new ArgumentNullException("communicationInterface");

            // Only create one protocol instance per interface
            lock (_protectInstances)
            {
                IEECommProtocol protItf = _protInstances.Find(p =>
                    (p.CommunicationInterface.InterfaceType == communicationInterface.InterfaceType &&
                    p.CommunicationInterface.InterfaceId == communicationInterface.InterfaceId));

                if (protItf != null)
                    return protItf;

                var prot = new EE31Protocol(communicationInterface);
                _protInstances.Add(prot);

                return prot;
            }
        }

        /// <summary>
        /// Remove EE31 protocol class.
        /// </summary>
        /// <param name="protocol">The protocol class</param>
        internal static void RemoveEE31Protocol(EE31Protocol protocol)
        {
            lock (_protectInstances)
            {
                _protInstances.Remove(protocol);
            }
        }
    }
}