using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EplusE.CommProtEE31")]

namespace EplusE.Measurement
{
    /// <summary>
    /// MVCode Translation Data class.
    /// </summary>
    public class MVCodeTranslationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MVCodeTranslationData"/> class.
        /// </summary>
        public MVCodeTranslationData()
        {
            EE31Index = -1;
            MVCode = MVCode.INVALID;
            MVCodeUS = MVCode.INVALID;
        }

        /// <summary>
        /// Gets or sets the EE31 index.
        /// </summary>
        /// <value>
        /// The EE31 index.
        /// </value>
        public int EE31Index { get; internal set; }

        /// <summary>
        /// Gets or sets the MV code.
        /// MVCode (default, Unit System SI or neutral)
        /// </summary>
        /// <value>
        /// The MV code.
        /// MVCode (default, Unit System SI or neutral)
        /// </value>
        public MVCode MVCode { get; internal set; }

        /// <summary>
        /// Gets or sets the MV code US.
        /// MVCode if Unit System is reported as US
        /// </summary>
        /// <value>
        /// The MV code US.
        /// MVCode if Unit System is reported as US
        /// </value>
        public MVCode MVCodeUS { get; internal set; }
    }
}