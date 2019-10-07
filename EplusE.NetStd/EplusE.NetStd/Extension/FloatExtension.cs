using System;

//using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Float extension methods.
    /// <locDE><para />Float Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class FloatExtension
    {
        #region ToDoubleWithFloatResolution

        /// <summary>
        /// Casts to double applying float resolution.
        /// <locDE><para />Wandelt in einen Double mit Float-Auflösung/Genauigkeit um.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Double with float resolution.<locDE><para />Double mit Float-Auflösung/Genauigkeit.</locDE></returns>
        public static double ToDoubleWithFloatResolution(this float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
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
        public static bool IsEqualByEpsilon(this float value, float other, float epsilon = 0.00001f)
        {
            // http://floating-point-gui.de/errors/comparison/

            if (value == other)
            {
                // Shortcut, handles infinities
                // Abkürzung, deckt auch Unendlichkeitswerte ab
                return true;
            }

            float absA = Math.Abs(value);
            float absB = Math.Abs(other);
            float diff = Math.Abs(value - other);

            if (value == 0 || other == 0 || diff < float.MinValue)
            {
                // A or B is zero or both are extremely close to it, relative error is less meaningful here
                // A oder B sind 0 oder beide sind Nahe bei 0, der relative Fehler ist vernachlässigbar
                return diff < (epsilon * float.MinValue);
            }
            else
            {
                // Use relative error
                // Prüfe relativen Fehler (Abweichung)
                return diff / (absA + absB) < epsilon;
            }
        }

        #endregion IsEqualByEpsilon

        #region ValueOrNullIfNaN

        /// <summary>
        /// Gets the float value or null if value is NaN.
        /// <locDE><para />Holt den Float-Wert oder Null falls Wert = NaN ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or float value.<locDE><para />Null oder Float-Wert.</locDE></returns>
        public static float? ValueOrNullIfNaN(this float value)
        {
            if (float.IsNaN(value))
                return null;

            return value;
        }

        /// <summary>
        /// Gets the float value or null if value is 0.
        /// <locDE><para />Holt den Float-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or float value.<locDE><para />Null oder Float-Wert.</locDE></returns>
        public static float? ValueOrNullIfNaN(this float? value)
        {
            if (null == value || float.IsNaN((float)value))
                return null;

            return value;
        }

        #endregion ValueOrNullIfNaN

        #region ValueOrNaNIfNull

        /// <summary>
        /// Gets the float value or NaN if null.
        /// <locDE><para />Holt den Float-Wert oder NaN bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Float value or 0 (if null).<locDE><para />Float-Wert oder NaN (falls Null).</locDE></returns>
        public static float ValueOrNaNIfNull(this float? value)
        {
            if (null == value || float.IsNaN((float)value))
                return float.NaN;

            return (float)value;
        }

        #endregion ValueOrNaNIfNull

        #region ValueOrNullIfZero

        /// <summary>
        /// Gets the float value or null if value is 0.
        /// <locDE><para />Holt den Float-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or float value.<locDE><para />Null oder Float-Wert.</locDE></returns>
        public static float? ValueOrNullIfZero(this float value)
        {
            if (0 == value)
                return null;

            return value;
        }

        /// <summary>
        /// Gets the float value or null if value is 0.
        /// <locDE><para />Holt den Float-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or float value.<locDE><para />Null oder Float-Wert.</locDE></returns>
        public static float? ValueOrNullIfZero(this float? value)
        {
            if (null == value || 0 == (float)value)
                return null;

            return value;
        }

        #endregion ValueOrNullIfZero

        #region ValueOrZeroIfNull

        /// <summary>
        /// Gets the float value or 0 if null.
        /// <locDE><para />Holt den Float-Wert oder 0 bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Float value or 0 (if null).<locDE><para />Float-Wert oder 0 (falls Null).</locDE></returns>
        public static float ValueOrZeroIfNull(this float? value)
        {
            if (null == value || 0 == (float)value)
                return 0;

            return (float)value;
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
        public static float Round(this float value, int fractionalDigits = 0)
        {
            // Explanation / Erklärung: http://www.vcskicks.com/rounding-doubles.php

            // AwayFromZero / Weg von der Null:
            //    2.5 =>  3
            //   -2.5 => -3
            return (float)Math.Round(value.ToDoubleWithFloatResolution(), fractionalDigits, MidpointRounding.AwayFromZero);
        }

        #endregion Round
    }
}