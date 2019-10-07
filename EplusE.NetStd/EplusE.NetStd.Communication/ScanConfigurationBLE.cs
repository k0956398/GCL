using System;
using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Scan configuration to scan BLE interface.
    /// </summary>
    public class ScanConfigurationBLE : IScanConfiguration
    {
        /// <summary>
        /// Scan configuration to scan all available BLE devices (Uuid == null) or to scan for one
        /// specific BLE device (Uuid != null).
        /// </summary>
        public ScanConfigurationBLE()
        {
            Uuid = null;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        public ScanConfigurationBLE(ScanConfigurationBLE other)
        {
            if (other != null)
            {
                Uuid = other.Uuid;
            }
        }

        /// <summary>
        /// Scan configuration to scan for device with given Uuid.
        /// </summary>
        /// <param name="uuid">Guid of device to scan for.</param>
        public ScanConfigurationBLE(Guid device)
        {
            Uuid = device;
        }

        public InterfaceType Type { get { return InterfaceType.BLE; } }

        public Guid? Uuid { get; private set; }

        public override bool Equals(object obj)
        {
            var bLE = obj as ScanConfigurationBLE;
            return bLE != null &&
                   Type == bLE.Type &&
                   EqualityComparer<Guid?>.Default.Equals(Uuid, bLE.Uuid);
        }

        public override int GetHashCode()
        {
            var hashCode = 609524679;
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid?>.Default.GetHashCode(Uuid);
            return hashCode;
        }

        public IScanConfiguration MakeCopy()
        {
            return new ScanConfigurationBLE(this);
        }
    }
}