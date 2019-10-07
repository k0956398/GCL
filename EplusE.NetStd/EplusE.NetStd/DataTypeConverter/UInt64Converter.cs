using System;

namespace EplusE.DataTypeConverter
{
    /// <summary>
    /// UInt64 conversion helpers, i.e. hex prefix aware parse method (0x...).
    /// <locDE><para />UInt64 Konvertierungshilfsmethoden, z.B. Parse-Methode mit HEX-Präfix-Unterstützung (0x...).</locDE>
    /// </summary>
    public static class UInt64Converter
    {
        #region Parse

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 Parse(string value, UInt64? defaultValue = null)
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
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 Parse(string value, UInt64? defaultValue, IFormatProvider ifp)
        {
            if (null == value && null != defaultValue)
                return (UInt64)defaultValue;

            UInt64 uint64Value;
            string work = value.ToLowerInvariant();
            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                if (null == ifp)
                {
                    if (UInt64.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier,
                        System.Globalization.CultureInfo.CurrentCulture, out uint64Value))
                        return uint64Value;
                }
                else
                {
                    if (UInt64.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out uint64Value))
                        return uint64Value;
                }

                if (null != defaultValue)
                    return (UInt64)defaultValue;
            }

            if (null == ifp)
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (UInt64.TryParse(work, out uint64Value))
                    return uint64Value;
            }
            else
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (UInt64.TryParse(work, System.Globalization.NumberStyles.Number, ifp, out uint64Value))
                    return uint64Value;
            }

            if (null != defaultValue)
                return (UInt64)defaultValue;
            throw new ArgumentException("UInt64Converter.Parse: Invalid value \"" + value + "\"");
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ParseInvariantCulture(object value, UInt64? defaultValue = null)
        {
            if (value is byte || value is sbyte || value is Int16 || value is Int32 || value is Int64 ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is double || value is float || value is decimal)
                return (UInt64)value;

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
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ParseInvariantCulture(string value, UInt64? defaultValue = null)
        {
            return Parse(value, defaultValue, CultureHelper.InvariantCulture);
        }

        #endregion Parse
    }
}