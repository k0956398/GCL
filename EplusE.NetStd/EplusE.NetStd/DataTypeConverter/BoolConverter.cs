using System;

namespace EplusE.DataTypeConverter
{
    /// <summary>
    /// Bool conversion helpers, i.e. hex prefix aware parse method (0x...).
    /// <locDE><para />Bool Konvertierungshilfsmethoden, z.B. Parse-Methode mit HEX-Präfix-Unterstützung (0x...).</locDE>
    /// </summary>
    public static class BoolConverter
    {
        #region Parse

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Bool value.<locDE><para />Bool Wert.</locDE></returns>
        public static bool Parse(string value, bool? defaultValue = null)
        {
            return Parse(value, defaultValue, null);
        }

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <param name="ifp">The format provider (or null).<locDE><para />Der Format-Provider (oder null).</locDE></param>
        /// <returns>Bool value.<locDE><para />Bool Wert.</locDE></returns>
        public static bool Parse(string value, bool? defaultValue, IFormatProvider ifp)
        {
            if (null == value && null != defaultValue)
                return (bool)defaultValue;

            string work = value.ToLowerInvariant();
            if (work.StartsWith("0x"))
                work = work.Mid(2);

            // true, wahr, yes, ja, high, on, ein
            if (StringHelper.StartsWithOneOf(ref work, new string[] { "1", "t", "w", "y", "j", "h", "on", "ein" }))
                return true;
            // false/falsch, no/nein, low, off, aus
            if (StringHelper.StartsWithOneOf(ref work, new string[] { "0", "f", "n", "l", "off", "aus" }))
                return false;

            bool flag;
            if (bool.TryParse(work, out flag))
                return flag;

            if (null != defaultValue)
                return (bool)defaultValue;
            throw new ArgumentException("BoolConverter.Parse: Invalid value \"" + value + "\"");
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Bool value.<locDE><para />Bool Wert.</locDE></returns>
        public static bool ParseInvariantCulture(object value, bool? defaultValue = null)
        {
            if (value is bool)
                return (bool)value;

            string strDefault = defaultValue.ToStringOrDefault();
            return Parse(value.ToStringOrDefault(strDefault), defaultValue, CultureHelper.InvariantCulture);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Bool value.<locDE><para />Bool Wert.</locDE></returns>
        public static bool ParseInvariantCulture(string value, bool? defaultValue = null)
        {
            return Parse(value, defaultValue, CultureHelper.InvariantCulture);
        }

        #endregion Parse
    }
}