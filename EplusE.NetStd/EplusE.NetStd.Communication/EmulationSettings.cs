namespace EplusE.NetStd.Communication
{
    /// <summary>
    /// Settings for emulated devices which will be generated through ScanConfigurationEmulation
    /// </summary>
    public class EmulationSettings
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EmulationSettings()
        {
            EmulateTimings = true;
            PreGenerateProbe = true;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other"></param>
        public EmulationSettings(EmulationSettings other)
        {
            if (other != null)
            {
                EmulateTimings = other.EmulateTimings;
                PreGenerateProbe = other.PreGenerateProbe;
            }
        }

        /// <summary>
        /// If true, emulation device also simulates timings
        /// </summary>
        public bool EmulateTimings { get; set; }

        /// <summary>
        /// If true, a pre configured probe will be generated
        /// </summary>
        public bool PreGenerateProbe { get; set; }

        public override bool Equals(object obj)
        {
            var settings = obj as EmulationSettings;
            return settings != null &&
                   EmulateTimings == settings.EmulateTimings &&
                   PreGenerateProbe == settings.PreGenerateProbe;
        }

        public override int GetHashCode()
        {
            var hashCode = -2100966574;
            hashCode = hashCode * -1521134295 + EmulateTimings.GetHashCode();
            hashCode = hashCode * -1521134295 + PreGenerateProbe.GetHashCode();
            return hashCode;
        }
    }
}