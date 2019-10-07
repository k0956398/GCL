using System;

//using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Double extension methods.
    /// <locDE><para />Double Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class DoubleExtension
    {
        #region ToDoubleWithFloatResolution

        /// <summary>
        /// Casts to double applying float resolution.
        /// <locDE><para />Wandelt in einen Double mit Float-Auflösung/Genauigkeit um.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Double with float resolution.<locDE><para />Double mit Float-Auflösung/Genauigkeit.</locDE></returns>
        public static double ToDoubleWithFloatResolution(this double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return value;

            // Roundtrip through Decimal data type prevents resolution errors when casting from float to double
            // Umweg über Decimal Datentyp verhindert Auflösungsfehler beim Umwandeln von Float in Double
            return (double)(Decimal)value;
        }

        #endregion ToDoubleWithFloatResolution

        #region IsEqualByEpsilon

        /// <summary>
        /// Determines whether is equal by epsilon.
        /// <locDE><para />Ermittelt, ob Gleichheit im Rahmen des angegebenen Epsilon-Wertes (max. Abweichung) gegeben ist.</locDE>
        /// </summary>
        /// <param name="value">Our own value.<locDE><para />Eigener Wert.</locDE></param>
        /// <param name="other">The other value.<locDE><para />Vergleichswert.</locDE></param>
        /// <param name="epsilon">The epsilon.<locDE><para />Epsilon (max. Abweichung).</locDE></param>
        /// <returns>True if equal.<locDE><para />True wenn gleich.</locDE></returns>
        public static bool IsEqualByEpsilon(this double value, double other, double epsilon = 0.00001)
        {
            // http://floating-point-gui.de/errors/comparison/

            if (value == other)
            {
                // Shortcut, handles infinities
                // Abkürzung, deckt auch Unendlichkeitswerte ab
                return true;
            }

            double absA = Math.Abs(value);
            double absB = Math.Abs(other);
            double diff = Math.Abs(value - other);

            if (value == 0 || other == 0 || diff < double.MinValue)
            {
                // A or B is zero or both are extremely close to it, relative error is less meaningful here
                // A oder B sind 0 oder beide sind Nahe bei 0, der relative Fehler ist vernachlässigbar
                return diff < (epsilon * double.MinValue);
            }
            else
            {
                // Use relative error
                // Prüfe relativen Fehler (Abweichung)
                return (diff / (absA + absB)) < epsilon;
            }
        }

        #endregion IsEqualByEpsilon

        #region ValueOrNullIfNaN

        /// <summary>
        /// Gets the double value or null if value is NaN.
        /// <locDE><para />Holt den Double-Wert oder Null falls Wert = NaN ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or double value.<locDE><para />Null oder Double-Wert.</locDE></returns>
        public static double? ValueOrNullIfNaN(this double value)
        {
            if (double.IsNaN(value))
                return null;

            return value;
        }

        /// <summary>
        /// Gets the double value or null if value is 0.
        /// <locDE><para />Holt den Double-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or double value.<locDE><para />Null oder Double-Wert.</locDE></returns>
        public static double? ValueOrNullIfNaN(this double? value)
        {
            if (null == value || double.IsNaN((double)value))
                return null;

            return value;
        }

        #endregion ValueOrNullIfNaN

        #region ValueOrNaNIfNull

        /// <summary>
        /// Gets the double value or NaN if null.
        /// <locDE><para />Holt den Double-Wert oder NaN bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Double value or 0 (if null).<locDE><para />Double-Wert oder NaN (falls Null).</locDE></returns>
        public static double ValueOrNaNIfNull(this double? value)
        {
            if (null == value || double.IsNaN((double)value))
                return double.NaN;

            return (double)value;
        }

        #endregion ValueOrNaNIfNull

        #region ValueOrNullIfZero

        /// <summary>
        /// Gets the double value or null if value is 0.
        /// <locDE><para />Holt den Double-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or double value.<locDE><para />Null oder Double-Wert.</locDE></returns>
        public static double? ValueOrNullIfZero(this double value)
        {
            if (0 == value)
                return null;

            return value;
        }

        /// <summary>
        /// Gets the double value or null if value is 0.
        /// <locDE><para />Holt den Double-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or double value.<locDE><para />Null oder Double-Wert.</locDE></returns>
        public static double? ValueOrNullIfZero(this double? value)
        {
            if (null == value || 0 == (double)value)
                return null;

            return value;
        }

        #endregion ValueOrNullIfZero

        #region ValueOrZeroIfNull

        /// <summary>
        /// Gets the double value or 0 if null.
        /// <locDE><para />Holt den Double-Wert oder 0 bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Double value or 0 (if null).<locDE><para />Double-Wert oder 0 (falls Null).</locDE></returns>
        public static double ValueOrZeroIfNull(this double? value)
        {
            if (null == value || 0 == (double)value)
                return 0;

            return (double)value;
        }

        #endregion ValueOrZeroIfNull

        #region Round

        /// <summary>
        /// Rounds a value away from zero.
        /// <locDE><para />Rundet einen Wert kaufmännisch (mit Berücksichtigung negativer Werte).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Zu rundender Wert.</locDE></param>
        /// <param name="fractionalDigits">The fractional digits.<locDE><para />Gewünschte Nachkommastellen.</locDE></param>
        /// <returns>Rounded value.<locDE><para />Gerundeter Wert.</locDE></returns>
        public static double Round(this double value, int fractionalDigits = 0)
        {
            // Explanation / Erklärung: http://www.vcskicks.com/rounding-doubles.php

            // AwayFromZero / Weg von der Null:
            //    2.5 =>  3
            //   -2.5 => -3
            return Math.Round(value, fractionalDigits, MidpointRounding.AwayFromZero);
        }

        #endregion Round
    }
}