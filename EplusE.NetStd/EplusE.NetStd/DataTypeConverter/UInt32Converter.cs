using System;

namespace EplusE.DataTypeConverter
{
    /// <summary>
    /// UInt32 conversion helpers, i.e. hex prefix aware parse method (0x...).
    /// <locDE><para />UInt32 Konvertierungshilfsmethoden, z.B. Parse-Methode mit HEX-Präfix-Unterstützung (0x...).</locDE>
    /// </summary>
    public static class UInt32Converter
    {
        #region Parse

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 Parse(string value, UInt32? defaultValue = null)
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
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 Parse(string value, UInt32? defaultValue, IFormatProvider ifp)
        {
            if (null == value && null != defaultValue)
                return (UInt32)defaultValue;

            UInt32 uint32Value;
            string work = value.ToLowerInvariant();
            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                if (null == ifp)
                {
                    if (UInt32.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier,
                        System.Globalization.CultureInfo.CurrentCulture, out uint32Value))
                        return uint32Value;
                }
                else
                {
                    if (UInt32.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out uint32Value))
                        return uint32Value;
                }

                if (null != defaultValue)
                    return (UInt32)defaultValue;
            }

            if (null == ifp)
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (UInt32.TryParse(work, out uint32Value))
                    return uint32Value;
            }
            else
            {
                #region Cut off at decimal point

                // If decimal point is included, parse only left part of it (ignore fractional part)
                int decimalPos = work.IndexOfAny(new char[] { '.', ',' });
                if (-1 != decimalPos)
                    work = work.Left(decimalPos);

                #endregion Cut off at decimal point

                if (UInt32.TryParse(work, System.Globalization.NumberStyles.Number, ifp, out uint32Value))
                    return uint32Value;
            }

            if (null != defaultValue)
                return (UInt32)defaultValue;
            throw new ArgumentException("UInt32Converter.Parse: Invalid value \"" + value + "\"");
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ParseInvariantCulture(object value, UInt32? defaultValue = null)
        {
            if (value is byte || value is sbyte || value is Int16 || value is Int32 || value is Int64 ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is double || value is float || value is decimal)
                return (UInt32)value;

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
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ParseInvariantCulture(string value, UInt32? defaultValue = null)
        {
            return Parse(value, defaultValue, CultureHelper.InvariantCulture);
        }

        #endregion Parse
    }
}