using System.Collections.Generic;

namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Scan configuration to create emulation devices without real hardware connection.
    /// </summary>
    public class ScanConfigurationEmulation : IScanConfiguration
    {
        /// <summary>
        /// Scan configuration to generate one default emulation device.
        /// </summary>
        public ScanConfigurationEmulation()
        {
            EmulationDevicesSettings = new List<EmulationSettings>() { new EmulationSettings() };
        }

        /// <summary>
        /// Scan configuration to generate emulationDevices.Count() number of emulation devices.
        /// </summary>
        /// <param name="emulationDevices">List of settings for emulation devices generated.</param>
        public ScanConfigurationEmulation(IEnumerable<EmulationSettings> emulationDevices)
        {
            EmulationDevicesSettings = new List<EmulationSettings>(emulationDevices);
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        public ScanConfigurationEmulation(ScanConfigurationEmulation other)
        {
            if (other != null)
            {
                EmulationDevicesSettings = new List<EmulationSettings>(other.EmulationDevicesSettings);
            }
        }

        /// <summary>
        /// List of settings for emulation devices generated. Each generated device might have its own setting.
        /// </summary>
        public IEnumerable<EmulationSettings> EmulationDevicesSettings { get; private set; }

        public InterfaceType Type { get { return InterfaceType.Emulation; } }

        public override bool Equals(object obj)
        {
            var emulation = obj as ScanConfigurationEmulation;
            return emulation != null &&
                   EqualityComparer<IEnumerable<EmulationSettings>>.Default.Equals(EmulationDevicesSettings, emulation.EmulationDevicesSettings) &&
                   Type == emulation.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 102531340;
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<EmulationSettings>>.Default.GetHashCode(EmulationDevicesSettings);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public IScanConfiguration MakeCopy()
        {
            return new ScanConfigurationEmulation(this);
        }
    }
}