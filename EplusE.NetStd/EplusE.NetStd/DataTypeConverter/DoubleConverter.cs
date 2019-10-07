using System;

namespace EplusE.DataTypeConverter
{
    /// <summary>
    /// Double conversion helpers, i.e. hex prefix aware parse method (0x...).
    /// <locDE><para />Double Konvertierungshilfsmethoden, z.B. Parse-Methode mit HEX-Präfix-Unterstützung (0x...).</locDE>
    /// </summary>
    public static class DoubleConverter
    {
        #region Public members

        /// <summary>
        /// Should both comma and dot symbol be interpreted as decimal point? No thousands delimiter possible in that case!
        /// <locDE><para />Sollen sowohl Komma als auch Punkt als Dezimaltrennzeichen behandelt werden? In diesem Fall ist kein Tausender-Trennzeichen möglich!</locDE>
        /// </summary>
        public static bool TreatCommaAndDotAsDecimalPoint = true;

        #endregion Public members

        #region TryParse

        /// <summary>
        /// Tries to parse the specified value (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string value, out double result)
        {
            return TryParse(value, out result, null);
        }

        /// <summary>
        /// Tries to parse the specified value (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <param name="ifp">The format provider (or null).<locDE><para />Der Format-Provider (oder null).</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string value, out double result, IFormatProvider ifp)
        {
            result = double.NaN;

            if (null == value)
                return false;

            if (null == ifp)
                ifp = System.Globalization.CultureInfo.CurrentCulture;

            System.Globalization.CultureInfo ci = null;
            if (ifp is System.Globalization.CultureInfo)
            {
                // Culture info given
                ci = (System.Globalization.CultureInfo)ifp;
            }

            string work = value.ToLowerInvariant();

            #region Treat comma and dot as decimal point

            if (TreatCommaAndDotAsDecimalPoint)
            {
                if (CultureHelper.InvariantCulture == ifp)
                {
                    // Invariant culture ("english"), convert any commas into dots
                    work = work.Replace(',', '.');
                }
                else
                {
                    try
                    {
                        if (null != ci)
                        {
                            // Culture info given, query decimal point character
                            switch (ci.NumberFormat.NumberDecimalSeparator ?? "")
                            {
                                case ",":
                                    // German/spanish culture, convert any dots into commas
                                    work = work.Replace('.', ',');
                                    break;

                                case ".":
                                    // English culture, convert any commas into dots
                                    work = work.Replace(',', '.');
                                    break;
                            }
                        }
                    }
                    catch { }
                }
            }

            #endregion Treat comma and dot as decimal point

            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                // Cannot use AllowHexSpecifier with floating point data type (throws not supported exception)
                Int64 dummy;
                if (Int64.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out dummy))
                {
                    result = (double)dummy;
                    return true;
                }
                return false;
            }

            return double.TryParse(work, System.Globalization.NumberStyles.Any, ifp, out result);
        }

        /// <summary>
        /// Tries to parse the specified value (also handles hex prefix "0x").
        /// <locDE><para />Versucht den angegebenen Wert zu parsen (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="result">The parsed value.<locDE><para />Der geparste Wert.</locDE></param>
        /// <returns>True if parsed successfully.<locDE><para />True wenn erfolgreich geparst.</locDE></returns>
        public static bool TryParseInvariantCulture(string value, out double result)
        {
            return TryParse(value, out result, CultureHelper.InvariantCulture);
        }

        #endregion TryParse

        #region Parse

        /// <summary>
        /// Parses the specified value (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double Parse(string value, double? defaultValue = null)
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
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double Parse(string value, double? defaultValue, IFormatProvider ifp)
        {
            if (null == value && null != defaultValue)
                return (double)defaultValue;

            System.Globalization.CultureInfo ci = null;
            if (ifp is System.Globalization.CultureInfo)
            {
                // Culture info given
                ci = (System.Globalization.CultureInfo)ifp;
            }

            double doubleValue;
            string work = value.ToLowerInvariant();

            #region Treat comma and dot as decimal point

            if (TreatCommaAndDotAsDecimalPoint)
            {
                if (CultureHelper.InvariantCulture == ifp)
                {
                    // Invariant culture ("english"), convert any commas into dots
                    work = work.Replace(',', '.');
                }
                else
                {
                    try
                    {
                        if (null != ci)
                        {
                            // Culture info given, query decimal point character
                            switch (ci.NumberFormat.NumberDecimalSeparator ?? "")
                            {
                                case ",":
                                    // German/spanish culture, convert any dots into commas
                                    work = work.Replace('.', ',');
                                    break;

                                case ".":
                                    // English culture, convert any commas into dots
                                    work = work.Replace(',', '.');
                                    break;
                            }
                        }
                    }
                    catch { }
                }
            }

            #endregion Treat comma and dot as decimal point

            if (work.StartsWith("0x"))
            {
                // Hex specifier prefix found, try to parse as hex number
                work = work.Mid(2);

                if (null == ifp)
                {
                    // Cannot use AllowHexSpecifier with floating point data type (throws not supported exception)
                    Int64 dummy;
                    if (Int64.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier,
                        System.Globalization.CultureInfo.CurrentCulture, out dummy))
                        return (double)dummy;
                }
                else
                {
                    // Cannot use AllowHexSpecifier with floating point data type (throws not supported exception)
                    Int64 dummy;
                    if (Int64.TryParse(work, System.Globalization.NumberStyles.AllowHexSpecifier, ifp, out dummy))
                        return (double)dummy;
                }

                if (null != defaultValue)
                    return (double)defaultValue;
            }

            if (null == ifp)
            {
                if (double.TryParse(work, out doubleValue))
                    return doubleValue;
            }
            else
            {
                if (double.TryParse(work, System.Globalization.NumberStyles.Any, ifp, out doubleValue))
                    return doubleValue;
            }

            if (null != defaultValue)
                return (double)defaultValue;
            throw new ArgumentException("DoubleConverter.Parse: Invalid value \"" + value + "\"");
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ParseInvariantCulture(object value, double? defaultValue = null)
        {
            if (value is byte || value is sbyte || value is Int16 || value is Int32 || value is Int64 ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is double || value is float || value is decimal)
                return (double)value;

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
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ParseInvariantCulture(string value, double? defaultValue = null)
        {
            return Parse(value, defaultValue, CultureHelper.InvariantCulture);
        }

        #endregion Parse
    }
}