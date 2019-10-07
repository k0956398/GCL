using System;
using System.Linq;
using System.Text;

namespace EplusE
{
    /// <summary>
    /// Provides string helper methods.
    /// <locDE><para />Stellt String Hilfsmethoden bereit.</locDE>
    /// </summary>
    public static class StringHelper
    {
        #region ToStringOrDefault[FromDict]

        /// <summary>
        /// Handles ToString() on an object potentially being null.
        /// <locDE><para />Behandelt ToString() auf einem Objekt, das Null sein könnte.</locDE>
        /// </summary>
        /// <param name="value">The value to convert to string.<locDE><para />Der Wert, der in einen String umgewandelt werden soll.</locDE></param>
        /// <param name="defaultValue">The default value (bool: Value for true).<locDE><para />Der Standardwert (bei Bool: Wert für true).</locDE></param>
        /// <param name="defaultValueTwo">The second default value (bool: Value for false).<locDE><para />Der zweite Standardwert (bei Bool: Wert für false).</locDE></param>
        /// <param name="culture">The culture, null means current culture (from OS).<locDE><para />Die Kultur, Null heißt: aktuelle Kultur (laut Betriebssystem).</locDE></param>
        /// <returns>
        /// ToString() result or default string (typ. empty).
        /// <locDE><para />Ergebnis von ToString() oder Standardwert (typ. leer).</locDE>
        /// </returns>
        public static string ToStringOrDefault(this object value, string defaultValue = "", string defaultValueTwo = "",
            System.Globalization.CultureInfo culture = null)
        {
            System.Collections.Generic.IDictionary<string, string> defaultValues = new System.Collections.Generic.Dictionary<string, string>();
            defaultValues[""] = defaultValue;
            defaultValues["DefaultValueTwo"] = defaultValueTwo;

            return ToStringOrDefaultFromDict(value, defaultValues, culture);
        }

        /// <summary>
        /// Handles ToString() on an object potentially being null.
        /// <locDE><para />Behandelt ToString() auf einem Objekt, das Null sein könnte.</locDE>
        /// </summary>
        /// <param name="value">The object value to convert to string.<locDE><para />Der Objektwert, der in einen String umgewandelt werden soll.</locDE></param>
        /// <param name="defaultValues">The default values for different data types, etc.<locDE><para />Die Standardwerte für verschiedene Datentypen, etc.</locDE></param>
        /// <param name="culture">The culture, null means current culture (from OS).<locDE><para />Die Kultur, Null heißt: aktuelle Kultur (laut Betriebssystem).</locDE></param>
        /// <returns>
        /// ToString() result or default string (typ. empty).
        /// <locDE><para />Ergebnis von ToString() oder Standardwert (typ. leer).</locDE>
        /// </returns>
        public static string ToStringOrDefaultFromDict(this object value,
            System.Collections.Generic.IDictionary<string, string> defaultValues, System.Globalization.CultureInfo culture = null)
        {
            string defaultValue = "";
            if (null != defaultValues && defaultValues.ContainsKey(""))
                defaultValue = defaultValues[""];
            else if (null != defaultValues && defaultValues.ContainsKey("DefaultValue"))
                defaultValue = defaultValues["DefaultValue"];

            if (null == value)
                return defaultValue;

            if (null == culture)
                culture = System.Globalization.CultureInfo.CurrentCulture;

            #region String data type

            if (value is string)
            {
                return (string)value;
            }

            #endregion String data type

            #region Bool data type

            if (value is bool || value is bool?)
            {
                if ((bool)value)
                {
                    // Bool is true
                    if (null != defaultValues && defaultValues.ContainsKey("BoolTrue"))
                        return defaultValues["BoolTrue"];
                    return defaultValue;
                }

                // Bool is false
                if (null != defaultValues && defaultValues.ContainsKey("BoolFalse"))
                    return defaultValues["BoolFalse"];
                if (null != defaultValues && defaultValues.ContainsKey("DefaultValueTwo"))
                    return defaultValues["DefaultValueTwo"];
                return defaultValue;
            }

            #endregion Bool data type

            #region Byte data type

            if (value is byte || value is byte?)
                return ((byte)value).ToString(culture);

            #endregion Byte data type

            #region Decimal data type

            if (value is Decimal || value is Decimal?)
                return ((Decimal)value).ToString(culture);

            #endregion Decimal data type

            #region Double data type

            if (value is double || value is double?)
                return ((double)value).ToString(culture);

            #endregion Double data type

            #region Float data type

            if (value is float || value is float?)
                return ((float)value).ToString(culture);

            #endregion Float data type

            #region Int32 data type

            if (value is Int32 || value is Int32?)
                return ((Int32)value).ToString(culture);

            #endregion Int32 data type

            #region Int64 data type

            if (value is Int64 || value is Int64?)
                return ((Int64)value).ToString(culture);

            #endregion Int64 data type

            #region UInt16 data type

            if (value is UInt16 || value is UInt16?)
                return ((UInt16)value).ToString(culture);

            #endregion UInt16 data type

            #region UInt32 data type

            if (value is UInt32 || value is UInt32?)
                return ((UInt32)value).ToString(culture);

            #endregion UInt32 data type

            #region UInt64 data type

            if (value is UInt64 || value is UInt64?)
                return ((UInt64)value).ToString(culture);

            #endregion UInt64 data type

            #region DateTime data type

            if (value is DateTime || value is DateTime?)
            {
                if (CultureHelper.InvariantCulture == culture)
                {
                    // ISO8601:
                    // 2014-07-22T08:18:20+00:00
                    // 2014-12-30T12:10:01.6304832Z
                    // 2014-12-30T13:10:01.6304832+01:00
                    return ((DateTime)value).ToStringIso8601();
                }

                // Call ToString() with current culture
                // ToString() mit aktueller Kultur aufrufen
                return ((DateTime)value).ToString(culture);
            }

            #endregion DateTime data type

            #region Version data type

            if (value is Version)
            {
                Version ver = (Version)value;
                return string.Format("{0:0}.{1:00}.{2:000}.{3:0000}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            }

            #endregion Version data type

            #region VersionEx data type

            if (value is VersionEx)
            {
                VersionEx ver = (VersionEx)value;
                return string.Format("{0:0}.{1:00}.{2:000}.{3:0000}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            }

            #endregion VersionEx data type

            #region Byte[] data type

            // Results in groups of two hex digits without delimiter/space
            // Ergibt Gruppen von zwei Hexziffern ohne Trennzeichen/Leerzeichen
            if (value is Byte[])
            {
                string result = "";
                foreach (byte by in (Byte[])value)
                    result = result + by.ToString("x2", culture);
                return result;
            }

            #endregion Byte[] data type

            #region IEnumerable data type

            if (value is System.Collections.IEnumerable)
            {
                // Results in flattened enumeraton of list elements (comma separated)
                // Ergibt "flachgedrückte" Auflistung der Listenelemente (Komma-getrennt)
                string delimiter = ",";
                if (null != defaultValues && defaultValues.ContainsKey("ListDelimiter"))
                    delimiter = defaultValues["ListDelimiter"];

                StringBuilder result = new StringBuilder();
                foreach (object obj in (System.Collections.IEnumerable)value)
                {
                    if (result.Length > 0)
                        result.Append(delimiter);

                    result.Append(ToStringOrDefaultFromDict(obj, defaultValues, culture));
                }
                return result.ToString();
            }

            #endregion IEnumerable data type

            // Not handled explicitly, so call ToString() with current culture
            // Nicht sonderbehandelt, daher ToString() mit aktueller Kultur aufrufen
            return value.ToString();
        }

        #endregion ToStringOrDefault[FromDict]

        #region ToStringInvariant

        /// <summary>
        /// Handles ToString() on an object potentially being null using invariant culture (english).
        /// <locDE><para />Behandelt ToString() auf einem Objekt, das Null sein könnte, mit fixierter englischer Kultureinstellung.</locDE>
        /// </summary>
        /// <param name="value">The value to convert to string.<locDE><para />Der Wert, der in einen String umgewandelt werden soll.</locDE></param>
        /// <returns>
        /// ToString() result or default string (typ. empty).
        /// <locDE><para />Ergebnis von ToString() oder Standardwert (typ. leer).</locDE>
        /// </returns>
        public static string ToStringInvariant(this object value)
        {
            #region ToStringOrDefault replacement values

            System.Collections.Generic.IDictionary<string, string> toStringDefaultValues = new System.Collections.Generic.Dictionary<string, string>();
            toStringDefaultValues[""] = "";
            toStringDefaultValues["BoolTrue"] = "1";
            toStringDefaultValues["BoolFalse"] = "0";

            #endregion ToStringOrDefault replacement values

            return ToStringOrDefaultFromDict(value, toStringDefaultValues, CultureHelper.InvariantCulture);
        }

        #endregion ToStringInvariant

        #region ToStringShortToday (DateTime)

        /// <summary>
        /// Handles ToString() for a DateTime, delivers time-only for date = today.
        /// <locDE><para />Behandelt ToString() für ein DateTime, liefert nur die Zeit für Datum = heute.</locDE>
        /// </summary>
        /// <param name="dt">The value to convert to string.<locDE><para />Der Wert, der in einen String umgewandelt werden soll.</locDE></param>
        /// <param name="forceLocalTime">Force local time (convert from UTC if necessary)?<locDE><para />Lokalzeit erzwingen (von UTC konvertieren, falls nötig)?</locDE></param>
        /// <param name="culture">The culture, null means current culture (from OS).<locDE><para />Die Kultur, Null heißt: aktuelle Kultur (laut Betriebssystem).</locDE></param>
        /// <returns>
        /// ToString() result or default string (typ. empty).
        /// <locDE><para />Ergebnis von ToString() oder Standardwert (typ. leer).</locDE>
        /// </returns>
        public static string ToStringShortToday(this DateTime dt, bool forceLocalTime = true, System.Globalization.CultureInfo culture = null)
        {
            if (null == culture)
                culture = System.Globalization.CultureInfo.CurrentCulture;

            if (forceLocalTime)
            {
                if (DateTimeKind.Utc == dt.Kind)
                    dt = dt.ToLocalTime();

                if (DateTimeKind.Unspecified == dt.Kind)
                    dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            }

            if (dt.IsTimeOnly() || dt.IsToday())
            {
                // "HH:mm"
                return dt.ToString(culture.DateTimeFormat.ShortTimePattern);
            }

            //"dd.MM.yyyy HH:mm"
            return dt.ToString(culture.DateTimeFormat.ShortDatePattern + " " + culture.DateTimeFormat.ShortTimePattern);
        }

        #endregion ToStringShortToday (DateTime)

        #region ToString (Version)

        /// <summary>
        /// Gets the version as formatted string, typically "V2.0".
        /// <locDE><para />Holt die Version als formatierter String, typischerweise "V2.0".</locDE>
        /// </summary>
        /// <param name="version">The version.<locDE><para />Die Version.</locDE></param>
        /// <param name="prefix">The prefix string, i.e. "V".<locDE><para />Der Präfix-String, z.B. "V".</locDE></param>
        /// <param name="fieldCount">The field count (1 = only major, 2 = major + minor, etc).
        /// <locDE><para />Die Feldanzahl (1 = nur Hauptversion, 2 = Haupt + Nebenversion, etc).</locDE></param>
        /// <returns>Version as formatted string.<locDE><para />Version als formatierter String.</locDE></returns>
        public static string ToString(this Version version, string prefix, int fieldCount = 2)
        {
            return ToString(version, prefix, fieldCount, false);
        }

        /// <summary>
        /// Gets the version as formatted string, typically "V2.0".
        /// <locDE><para />Holt die Version als formatierter String, typischerweise "V2.0".</locDE>
        /// </summary>
        /// <param name="version">The version.<locDE><para />Die Version.</locDE></param>
        /// <param name="prefix">The prefix string, i.e. "V".<locDE><para />Der Präfix-String, z.B. "V".</locDE></param>
        /// <param name="fieldCount">The field count (1 = only major, 2 = major + minor, etc).
        /// <locDE><para />Die Feldanzahl (1 = nur Hauptversion, 2 = Haupt + Nebenversion, etc).</locDE></param>
        /// <param name="includeNonZeroFields">If true, non-zero fields beyond <paramref name="fieldCount" /> are also included (i.e. non-zero build/revision field although <paramref name="fieldCount" /> is 2).
        /// <locDE><para />Falls true, werden auch Felder über den <paramref name="fieldCount" /> hinaus geliefert, so sie einen Wert ungleich 0 enthalten (z.B. Build/Revision Nummer ungleich 0, obwohl <paramref name="fieldCount" /> gleich 2 ist).</locDE></param>
        /// <returns>Version as formatted string.<locDE><para />Version als formatierter String.</locDE></returns>
        public static string ToString(this Version version, string prefix, int fieldCount, bool includeNonZeroFields)
        {
            if (includeNonZeroFields)
            {
                switch (fieldCount)
                {
                    case 1:
                        // Major version only
                        if (version.Minor > 0)
                        {
                            fieldCount++;

                            if (version.Build > 0)
                            {
                                fieldCount++;

                                if (version.Revision > 0)
                                    fieldCount++;
                            }
                        }
                        break;

                    case 2:
                        // Major + Minor version only
                        if (version.Build > 0)
                        {
                            fieldCount++;

                            if (version.Revision > 0)
                                fieldCount++;
                        }
                        break;

                    case 3:
                        // Major + Minor + Build/Revision version only
                        if (version.Revision > 0)
                            fieldCount++;
                        break;
                }
            }

            string value = version.ToString(fieldCount);

            if (string.IsNullOrWhiteSpace(prefix))
                return value;
            return prefix + value;
        }

        #endregion ToString (Version)

        #region ToStringCompact (Guid)

        /// <summary>
        /// Gets a GUID as compact string, i.e. "{DAB56B84-3EEA-49D2-AB56-82789CA8C383}" --> "dab56b84-3eea-49d2-ab56-82789ca8c383".
        /// <locDE><para />Holt eine GUID als kompakten String, z.B. "{DAB56B84-3EEA-49D2-AB56-82789CA8C383}" --> "dab56b84-3eea-49d2-ab56-82789ca8c383".</locDE>
        /// </summary>
        /// <param name="value">The Guid value.<locDE><para />Der Guid-Wert.</locDE></param>
        /// <returns>Guid as compact string.<locDE><para />Guid als kompakter String.</locDE></returns>
        public static string ToStringCompact(this Guid value)
        {
#pragma warning disable 612, 618    // disable obsolete warning
            return StdGuidToString(value);
#pragma warning restore 612, 618    // restore obsolete warning
        }

        #endregion ToStringCompact (Guid)

        // NOTE: See DateTimeHelper for more ToString() extension methods.
        // Hinweis: Siehe DateTimeHelper für weitere ToString() Erweiterungsmethoden.

        #region ToBool, ToByte, etc.

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Bool value.<locDE><para />Bool Wert.</locDE></returns>
        public static bool ToBool(this string value, bool? defaultValue = null)
        {
            return DataTypeConverter.BoolConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Byte value.<locDE><para />Byte Wert.</locDE></returns>
        public static byte ToByte(this string value, byte? defaultValue = null)
        {
            return DataTypeConverter.ByteConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Decimal value.<locDE><para />Double Wert.</locDE></returns>
        public static decimal ToDecimal(this string value, decimal? defaultValue = null)
        {
            return DataTypeConverter.DecimalConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Double value.<locDE><para />Double Wert.</locDE></returns>
        public static double ToDouble(this string value, double? defaultValue = null)
        {
            return DataTypeConverter.DoubleConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Float value.<locDE><para />Float Wert.</locDE></returns>
        public static float ToFloat(this string value, float? defaultValue = null)
        {
            return DataTypeConverter.FloatConverter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static int ToInt(this string value, int? defaultValue = null)
        {
            return DataTypeConverter.Int32Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Int32 value.<locDE><para />Int32 Wert.</locDE></returns>
        public static Int32 ToInt32(this string value, Int32? defaultValue = null)
        {
            return DataTypeConverter.Int32Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static Int64 ToInt64(this string value, Int64? defaultValue = null)
        {
            return DataTypeConverter.Int64Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>Int64 value.<locDE><para />Int64 Wert.</locDE></returns>
        public static long ToLong(this string value, long? defaultValue = null)
        {
            return DataTypeConverter.Int64Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt16 value.<locDE><para />UInt16 Wert.</locDE></returns>
        public static UInt16 ToUInt16(this string value, UInt16? defaultValue = null)
        {
            return DataTypeConverter.UInt16Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt32 value.<locDE><para />UInt32 Wert.</locDE></returns>
        public static UInt32 ToUInt32(this string value, UInt32? defaultValue = null)
        {
            return DataTypeConverter.UInt32Converter.ParseInvariantCulture(value, defaultValue);
        }

        /// <summary>
        /// Parses the specified value using invariant culture (also handles hex prefix "0x").
        /// <locDE><para />Parst den angegebenen Wert mit fixierter englischer Kultureinstellung (berücksichtigt auch HEX-Präfix "0x").</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="defaultValue">The default value if <paramref name="value"/> is empty or invalid. Null means throw exception for invalid value.
        /// <locDE><para />Der Standardwert, falls <paramref name="value"/> leer oder ungültig ist. Null heißt, dass bei ungültigem Wert eine Exception geworfen werden soll.</locDE></param>
        /// <returns>UInt64 value.<locDE><para />UInt64 Wert.</locDE></returns>
        public static UInt64 ToUInt64(this string value, UInt64? defaultValue = null)
        {
            return DataTypeConverter.UInt64Converter.ParseInvariantCulture(value, defaultValue);
        }

        #endregion ToBool, ToByte, etc.

        #region StdGuidToString

        /// <summary>
        /// Standard GUID to string conversion, i.e. "{DAB56B84-3EEA-49D2-AB56-82789CA8C383}" --> "dab56b84-3eea-49d2-ab56-82789ca8c383".
        /// <locDE><para />Standard GUID zu String Umwandlung, z.B. "{DAB56B84-3EEA-49D2-AB56-82789CA8C383}" --> "dab56b84-3eea-49d2-ab56-82789ca8c383".</locDE>
        /// </summary>
        /// <param name="value">The Guid value.<locDE><para />Der Guid-Wert.</locDE></param>
        /// <returns>Guid as formatted string.<locDE><para />Guid als formatierter String.</locDE></returns>
        [Obsolete("StdGuidToString is deprecated, please use Guid extension method ToStringCompact instead.   StdGuidToString wird aufgelassen, bitte stattdessen Guid Extension Methode ToStringCompact verwenden.")]
        public static string StdGuidToString(Guid value)
        {
            // Remove leading/trailing brackets, whitespaces, CR/LF and remove any whitespaces within string
            // Entferne alle führenden/abschließenden Klammern, Leerzeichen, Tabs, CR/LF und entferne alle Leerzeichen/Tabs im String
            return value.ToString().Trim(new char[] { '{', '}', '[', ']', '(', ')', ' ', '\t', '\r', '\n' }).ReplaceMulti(" ||\t|").ToLower();
        }

        #endregion StdGuidToString

        #region EmptyToNull

        /// <summary>
        /// Translates an empty value to null and the keywords/placeholders [EMPTY] or [LEER] to empty value.
        /// <locDE><para />Übersetzt einen Leerstring zu Null und die Schlüsselbegriffe/Platzhalter [EMPTY] oder [LEER] zu einem Leerstring.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="emptyKeywords">The empty keywords/placeholders. Specify empty string array if none allowed.
        /// <locDE><para />Die Liste der Schlüsselbegriffe/Platzhalter. Eine leere Liste angeben falls keine erlaubt.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string EmptyToNull(this string value, string[] emptyKeywords = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (null == emptyKeywords)
                emptyKeywords = new string[] { "[EMPTY]", "[LEER]" };

            if (null != emptyKeywords && 0 != emptyKeywords.Length)
            {
                // Check for empty keyword
                if (emptyKeywords.Contains(value, StringComparer.OrdinalIgnoreCase))
                    return string.Empty;
            }

            // Pass original value
            return value;
        }

        #endregion EmptyToNull

        #region ReplaceChars

        /// <summary>
        /// Replaces the specified characters by string <paramref name="replaceWith"/>.
        /// <locDE><para />Ersetzt die angegebenen Zeichen durch den String <paramref name="replaceWith"/>.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="charsToReplace">The characters to replace.<locDE><para />Die zu ersetzenden Zeichen.</locDE></param>
        /// <param name="replaceWith">The new value to insert instead of replaced characters (null/empty to skip).
        /// <locDE><para />Der neue Wert, der anstelle der entfernten Zeichen eingesetzt wird (Null/leer entfernt ersatzlos).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string ReplaceChars(this string value, char[] charsToReplace, string replaceWith = null)
        {
            if (string.IsNullOrWhiteSpace(value) || null == charsToReplace)
                return value;

            foreach (char charToRemove in charsToReplace)
                value = value.Replace(new string(charToRemove, 1), (replaceWith ?? ""));
            return value;
        }

        #endregion ReplaceChars

        #region ReplaceMulti (multiple replacements coded in a string / Mehrfachersetzung codiert in einem String)

        /// <summary>
        /// Replaces multiple old/new string pairs by default separated by Pipe (|), i.e. "old|new|\t|_". Case sensitive!
        /// <locDE><para />Ersetzt mehrere alt/neu String-Paare, standardmäßig durch Pipe (|) getrennt, z.B. "alt|neu|\t|_". Groß-/Kleinschreibung wird beachtet!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="replaceEncoded">The encoded replacements, i.e. "old|new|\t|| |".<locDE><para />Die codierten Ersetzungen, z.B. "alt|neu|\t|| |".</locDE></param>
        /// <param name="delimiter">The delimiter, default is pipe character (|).<locDE><para />Das Trennzeichen, standardmäßig Pipe-Zeichen (|).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string ReplaceMulti(this string value, string replaceEncoded, string delimiter = "|")
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(replaceEncoded))
                return value;

            string[] splitItems = replaceEncoded.Split(new string[] { delimiter }, StringSplitOptions.None);
            int splitItemsCount = splitItems.Length;
            if (0 == splitItemsCount) // || 0 != (splitItemsCount % 2))
                return value;

            if (2 == splitItemsCount)
            {
                // string.Replace is faster, overhead for StringBuilder would be counterproductive
                // string.Replace ist schneller, das Drumherum für StringBuilder wäre kontraproduktiv
                string oldValue = splitItems[0];
                string newValue = splitItems[1];

                return value.Replace(oldValue, newValue);
            }

            // StringBuilder speedup might be worth the overhead
            // StringBuilder Geschwindigkeitsgewinn könnte das Drumherum wert sein
            StringBuilder sb = new StringBuilder(value, value.Length * 2);
            for (int idx = 0; (idx + 1) < splitItemsCount; idx += 2)
            {
                string oldValue = splitItems[idx];
                string newValue = splitItems[idx + 1];

                sb.Replace(oldValue, newValue);
            }
            return sb.ToString();
        }

        #endregion ReplaceMulti (multiple replacements coded in a string / Mehrfachersetzung codiert in einem String)

        #region RemoveAllButLetters

        /// <summary>
        /// Removes all chars other than letters. German Umlauts etc. will be removed (if not given in <paramref name="moreAllowedChars"/>)!
        /// <locDE><para />Entfernt alle Zeichen, die keine Buchstaben sind. Umlaute zählen nicht als Zeichen und werden entfernt (falls nicht in <paramref name="moreAllowedChars"/> angegeben)!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed characters, i.e. German Umlauts?<locDE><para />Die zusätzlich erlaubten Zeichen, z.B. Umlaute?</locDE></param>
        /// <param name="replaceWith">The new value to insert instead of replaced characters (null/empty to skip).
        /// <locDE><para />Der neue Wert, der anstelle der entfernten Zeichen eingesetzt werden soll (Null/leer heißt ersatzlos).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RemoveAllButLetters(this string value, string moreAllowedChars = null, string replaceWith = null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in value)
            {
                if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') ||
                    (null != moreAllowedChars && moreAllowedChars.Contains(ch.ToString())))
                {
                    // Allowed character, add to result
                    sb.Append(ch);
                }
                else
                {
                    // Not allowed character, skip (or replace)
                    if (!string.IsNullOrEmpty(replaceWith))
                        sb.Append(replaceWith);
                }
            }
            return sb.ToString();
        }

        #endregion RemoveAllButLetters

        #region RemoveAllButDigits

        /// <summary>
        /// Removes all chars other than digits. Also keeps chars given in <paramref name="moreAllowedChars"/>.
        /// <locDE><para />Entfernt alle Zeichen, die keine Ziffern sind. Behält auch Zeichen, die in <paramref name="moreAllowedChars"/> angegeben wurden.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed characters.<locDE><para />Die zusätzlich erlaubten Zeichen.</locDE></param>
        /// <param name="replaceWith">The new value to insert instead of replaced characters (null/empty to skip).
        /// <locDE><para />Der neue Wert, der anstelle der entfernten Zeichen eingesetzt werden soll (Null/leer heißt ersatzlos).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RemoveAllButDigits(this string value, string moreAllowedChars = null, string replaceWith = null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in value)
            {
                if ((ch >= '0' && ch <= '9') ||
                    (null != moreAllowedChars && moreAllowedChars.Contains(ch.ToString())))
                {
                    // Allowed character, add to result
                    sb.Append(ch);
                }
                else
                {
                    // Not allowed character, skip (or replace)
                    if (!string.IsNullOrEmpty(replaceWith))
                        sb.Append(replaceWith);
                }
            }
            return sb.ToString();
        }

        #endregion RemoveAllButDigits

        #region RemoveAllButAlphaNum

        /// <summary>
        /// Removes all chars other than letters and digits. German Umlauts etc. will be removed (if not given in <paramref name="moreAllowedChars"/>)!
        /// <locDE><para />Entfernt alle Zeichen, die keine Buchstaben oder Ziffern sind. Umlaute zählen nicht als Zeichen und werden entfernt (falls nicht in <paramref name="moreAllowedChars"/> angegeben)!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed characters, i.e. German Umlauts?<locDE><para />Die zusätzlich erlaubten Zeichen, z.B. Umlaute?</locDE></param>
        /// <param name="replaceWith">The new value to insert instead of replaced characters (null/empty to skip).
        /// <locDE><para />Der neue Wert, der anstelle der entfernten Zeichen eingesetzt werden soll (Null/leer heißt ersatzlos).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RemoveAllButAlphaNum(this string value, string moreAllowedChars = null, string replaceWith = null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in value)
            {
                if ((ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') ||
                    (null != moreAllowedChars && moreAllowedChars.Contains(ch.ToString())))
                {
                    // Allowed character, add to result
                    sb.Append(ch);
                }
                else
                {
                    // Not allowed character, skip (or replace)
                    if (!string.IsNullOrEmpty(replaceWith))
                        sb.Append(replaceWith);
                }
            }
            return sb.ToString();
        }

        #endregion RemoveAllButAlphaNum

        #region RemoveLeading

        /// <summary>
        /// Removes the specified values from start of string.
        /// <locDE><para />Entfernt die angegebenen Werte vom Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="valuesToTrim">The values to remove.<locDE><para />Die zu entfernenden Werte.</locDE></param>
        /// <param name="comparison">The comparison method.<locDE><para />Die Vergleichsmethode.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RemoveLeading(this string value, string[] valuesToTrim, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (null == value || null == valuesToTrim)
                return null;

            while (true)
            {
                bool trimDone = false;

                foreach (string valueToTrim in valuesToTrim)
                {
                    if (value.StartsWith(valueToTrim, comparison))
                    {
                        value = value.Right(value.Length - valueToTrim.Length);
                        trimDone = true;
                    }
                }

                if (!trimDone)
                    break;
            }
            return value;
        }

        #endregion RemoveLeading

        #region RemoveTrailing

        /// <summary>
        /// Removes the specified values from end of string.
        /// <locDE><para />Entfernt die angegebenen Werte vom Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="valuesToTrim">The values to remove.<locDE><para />Die zu entfernenden Werte.</locDE></param>
        /// <param name="comparison">The comparison method.<locDE><para />Die Vergleichsmethode.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RemoveTrailing(this string value, string[] valuesToTrim, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (null == value || null == valuesToTrim)
                return null;

            while (true)
            {
                bool trimDone = false;

                foreach (string valueToTrim in valuesToTrim)
                {
                    if (value.EndsWith(valueToTrim, comparison))
                    {
                        value = value.Left(value.Length - valueToTrim.Length);
                        trimDone = true;
                    }
                }

                if (!trimDone)
                    break;
            }
            return value;
        }

        #endregion RemoveTrailing

        #region EqualsSafe

        /// <summary>
        /// Null-safe Equals method.
        /// <locDE><para />Null-sicherer Vergleich auf Gleichheit.</locDE>
        /// </summary>
        /// <param name="value">The value (may be null).<locDE><para />Der Wert (darf Null sein).</locDE></param>
        /// <param name="itemToCompare">The item to compare (may be null).<locDE><para />Der Vergleichswert (darf Null sein).</locDE></param>
        /// <returns>True if the values are equal.<locDE><para />True falls die Werte gleich sind.</locDE></returns>
        public static bool EqualsSafe(this string value, object itemToCompare)
        {
            if (null == value)
            {
                if (null == itemToCompare)
                {
                    // Both null, so they are equal
                    // Beide Null, also sind sie gleich
                    return true;
                }
                // Not equal
                // Nicht gleich
                return false;
            }

            if (null == itemToCompare)
            {
                // Not equal
                // Nicht gleich
                return false;
            }

            return value.Equals(itemToCompare);
        }

        #endregion EqualsSafe

        #region EqualsSafeIgnoreCase

        /// <summary>
        /// Null-safe Equals method which ignores (upper/lower) case.
        /// <locDE><para />Null-sicherer Vergleich auf Gleichheit, ignoriert Groß-/Kleinschreibung.</locDE>
        /// </summary>
        /// <param name="value">The value (may be null).<locDE><para />Der Wert (darf Null sein).</locDE></param>
        /// <param name="itemToCompare">The item to compare (may be null).<locDE><para />Der Vergleichswert (darf Null sein).</locDE></param>
        /// <returns>True if the values are equal.<locDE><para />True falls die Werte gleich sind.</locDE></returns>
        public static bool EqualsSafeIgnoreCase(this string value, object itemToCompare)
        {
            if (null == value)
            {
                if (null == itemToCompare)
                {
                    // Both null, so they are equal
                    // Beide Null, also sind sie gleich
                    return true;
                }
                // Not equal
                // Nicht gleich
                return false;
            }

            if (null == itemToCompare)
            {
                // Not equal
                // Nicht gleich
                return false;
            }

            return value.Equals(itemToCompare as string, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Null-safe Equals method which ignores (upper/lower) case. If any of the mentioned strings fits, the result is true (is equal).
        /// <locDE><para />Null-sicherer Vergleich auf Gleichheit, ignoriert Groß-/Kleinschreibung. Falls irgendein angegebener String passt, ist das Ergebnis true (ist gleich).</locDE>
        /// </summary>
        /// <param name="value">The value (may be null).<locDE><para />Der Wert (darf Null sein).</locDE></param>
        /// <param name="itemsToCompare">The items to compare if one of them is equal to <paramref name="value"/>.
        /// <locDE><para />Die Vergleichswerte zur Prüfung, ob einer davon gleich <paramref name="value"/> ist.</locDE></param>
        /// <returns>True if at least one string is equal.<locDE><para />True falls zumindest ein String gleich ist.</locDE></returns>
        public static bool EqualsSafeIgnoreCase(this string value, string[] itemsToCompare)
        {
            if (null == itemsToCompare)
            {
                return false;
            }

            foreach (string itemToCompare in itemsToCompare)
            {
                if (null == value)
                {
                    if (null == itemToCompare)
                    {
                        // Both null, so they are equal
                        return true;
                    }
                }

                if (null == itemToCompare)
                {
                    // Not equal
                    return false;
                }

                if (value.Equals(itemToCompare, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        #endregion EqualsSafeIgnoreCase

        #region StartsWithOneOf

        /// <summary>
        /// Tries to find one of the given keys (and removes the one found).
        /// Returns true if any key was found (and removed).
        /// <locDE><para />Versucht, einen der angegebenen Schlüsselbegriffe zu finden (und entfernt diesen dann).
        /// Liefert true, falls ein Schlüsselbegriff gefunden (und entfernt) wurde.</locDE>
        /// </summary>
        /// <param name="value">The value to search within/remove from.<locDE><para />Der zu durchsuchende (und bearbeitende) Wert.</locDE></param>
        /// <param name="keys">The keys to look for.<locDE><para />Die in Frage kommenden Schlüsselbegriffe.</locDE></param>
        /// <param name="removeFound">Should the found key be removed?<locDE><para />Soll der gefundene Wert entfernt werden?</locDE></param>
        /// <param name="trimStart">Should the value be trimmed after key was removed?<locDE><para />Soll der Wert nach der Entfernung getrimmt werden?</locDE></param>
        /// <returns>True if any key was found (and removed).<locDE><para />True, falls ein Schlüsselbegriff gefunden (und entfernt) wurde.</locDE></returns>
        public static bool StartsWithOneOf(ref string value, string[] keys, bool removeFound = true, bool trimStart = true)
        {
            string foundKey;
            return StartsWithOneOf(ref value, keys, out foundKey, removeFound, trimStart);
        }

        /// <summary>
        /// Tries to find one of the given keys (and removes the one found).
        /// Returns true if any key was found (and removed).
        /// <locDE><para />Versucht, einen der angegebenen Schlüsselbegriffe zu finden (und entfernt diesen dann).
        /// Liefert true, falls ein Schlüsselbegriff gefunden (und entfernt) wurde.</locDE>
        /// </summary>
        /// <param name="value">The value to search within/remove from.<locDE><para />Der zu durchsuchende (und bearbeitende) Wert.</locDE></param>
        /// <param name="keys">The keys to look for.<locDE><para />Die in Frage kommenden Schlüsselbegriffe.</locDE></param>
        /// <param name="foundKey">The found key.<locDE><para />Der gefundene Schlüsselbegriff.</locDE></param>
        /// <param name="removeFound">Should the found key be removed?<locDE><para />Soll der gefundene Wert entfernt werden?</locDE></param>
        /// <param name="trimStart">Should the value be trimmed after key was removed?<locDE><para />Soll der Wert nach der Entfernung getrimmt werden?</locDE></param>
        /// <returns>True if any key was found (and removed).<locDE><para />True, falls ein Schlüsselbegriff gefunden (und entfernt) wurde.</locDE></returns>
        public static bool StartsWithOneOf(ref string value, string[] keys, out string foundKey, bool removeFound = true, bool trimStart = true)
        {
            foundKey = null;

            foreach (string key in keys)
            {
                if (value.StartsWith(key))
                {
                    if (removeFound)
                    {
                        foundKey = key;
                        value = value.Substring(key.Length);
                        if (trimStart)
                            value = value.TrimStart();
                    }
                    return true;
                }
            }
            return false;
        }

        #endregion StartsWithOneOf

        #region ContainsOnlyDigits

        /// <summary>
        /// Determines whether specified string contains only digits.
        /// NOTE: Any additionally acceptable characters like decimal separator must be allowed explicitly!
        /// <locDE><para />Ermittelt, ob der angegebene String nur Ziffern enthält.
        /// Hinweis: Alle weiteren zulässigen Zeichen, wie z.B. Dezimaltrennzeichen, müssen explizit als erlaubt angegeben werden!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed chars.<locDE><para />Die weiteren zulässigen Zeichen.</locDE></param>
        /// <returns>True if the string only contains allowed chars.<locDE><para />True, falls der String nur erlaubte Zeichen enthält.</locDE></returns>
        public static bool ContainsOnlyDigits(this string value, string moreAllowedChars = null)
        {
            if (null == value)
                return false;

            foreach (char ch in value)
            {
                if ((ch < '0' || ch > '9') && (null == moreAllowedChars || !moreAllowedChars.Contains(ch.ToString())))
                    return false;
            }
            return true;
        }

        #endregion ContainsOnlyDigits

        #region ContainsOnlyLetters

        /// <summary>
        /// Determines whether specified string contains only letters.
        /// NOTE: Any additionally acceptable characters like German Umlauts/punctuation must be allowed explicitly!
        /// <locDE><para />Ermittelt, ob der angegebene String nur Buchstaben enthält.
        /// Hinweis: Alle weiteren zulässigen Zeichen, wie z.B. Umlaute/Satzzeichen, müssen explizit als erlaubt angegeben werden!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed chars.<locDE><para />Die weiteren zulässigen Zeichen.</locDE></param>
        /// <returns>True if the string only contains allowed chars.<locDE><para />True, falls der String nur erlaubte Zeichen enthält.</locDE></returns>
        public static bool ContainsOnlyLetters(this string value, string moreAllowedChars = null)
        {
            if (null == value)
                return false;

            foreach (char ch in value)
            {
                if (!char.IsLetter(ch) && (null == moreAllowedChars || !moreAllowedChars.Contains(ch.ToString())))
                    return false;
            }
            return true;
        }

        #endregion ContainsOnlyLetters

        #region IsWhitespace

        /// <summary>
        /// Determines whether character is a whitespace character.
        /// NOTE: Any additionally acceptable characters like decimal separator must be allowed explicitly!
        /// <locDE><para />Ermittelt, ob ein Zeichen ein Leerzeichen/Tab ist.
        /// Hinweis: Alle weiteren zulässigen Zeichen, wie z.B. Dezimaltrennzeichen, müssen explizit als erlaubt angegeben werden!</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreAllowedChars">The additionally allowed chars.<locDE><para />Die weiteren zulässigen Zeichen.</locDE></param>
        /// <returns>True if the char is whitespace/allowed char.<locDE><para />True, falls das Zeichen ein Leerzeichen/Tab/erlaubtes Zeichen ist.</locDE></returns>
        public static bool IsWhitespace(this char value, string moreAllowedChars = null)
        {
            //if (null == value)
            //    return false;

            if (' ' != value && '\t' != value && (null == moreAllowedChars || !moreAllowedChars.Contains(value.ToString())))
                return false;
            return true;
        }

        #endregion IsWhitespace

        #region MatchesWildcard

        /// <summary>
        /// Checks if value matches any specified wildcard pattern(s), i.e. "*.jpg|*.bmp".
        /// <locDE><para />Prüft, ob der Wert einer der angegebenen Wildcard-Masken entspricht, z.B. "*.jpg|*.bmp".</locDE>
        /// </summary>
        /// <param name="value">The value to check if it matches against any specified wildcard pattern.
        /// <locDE><para />Der Wert, der gegen die angegebenen Wildcard-Masken geprüft werden soll.</locDE></param>
        /// <param name="pattern">The pattern(s), i.e. "*.jpg|*.bmp".<locDE><para />Die Masken, z.B. "*.jpg|*.bmp".</locDE></param>
        /// <param name="windowsStyle">Evaluate pattern in Windows style (true) or Unix style (false)?
        /// <locDE><para />Auswertung nach Windows-Art (true) oder Unix-Art (false)?</locDE></param>
        /// <returns>True if value matches any wildcard pattern, otherwise false.
        /// <locDE><para />True, falls der Wert einer Wildcard-Maske entspricht, sonst false.</locDE></returns>
        public static bool MatchesWildcard(this string value, string pattern, bool windowsStyle = true)
        {
            if (null == value || string.IsNullOrWhiteSpace(pattern))
                return false;

            string[] masksToCheck = pattern.Split(new string[] { "|", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (null == masksToCheck)
                return false;

            foreach (string mask in masksToCheck)
            {
                if (string.IsNullOrWhiteSpace(mask))
                    continue;

                if (windowsStyle)
                {
                    if (MatchesWildcardWindowsStyle(value, mask))
                        return true;
                }
                else
                {
                    if (MatchesWildcardUnixStyle(value, mask))
                        return true;
                }
            }
            return false;
        }

        #endregion MatchesWildcard

        #region MatchesWildcardWindowsStyle

        /// <summary>
        /// Checks using Windows style, if value matches the specified wildcard pattern, i.e. "*.jpg".
        /// <locDE><para />Prüft nach Windows-Art, ob der Wert einer der angegebenen Wildcard-Maske entspricht, z.B. "*.jpg".</locDE>
        /// </summary>
        /// <param name="value">The value to check if it matches against the specified wildcard pattern.
        /// <locDE><para />Der Wert, der gegen die angegebenen Wildcard-Maske geprüft werden soll.</locDE></param>
        /// <param name="pattern">The pattern, i.e. "*.jpg".<locDE><para />Die Maske, z.B. "*.jpg".</locDE></param>
        /// <returns>True if value matches any wildcard pattern, otherwise false.
        /// <locDE><para />True, falls der Wert der Wildcard-Maske entspricht, sonst false.</locDE></returns>
        private static bool MatchesWildcardWindowsStyle(string value, string pattern)
        {
            // http://stackoverflow.com/a/16488364

            var dotdot = pattern.IndexOf("..", StringComparison.Ordinal);
            if (dotdot >= 0)
            {
                for (var i = dotdot; i < pattern.Length; i++)
                    if (pattern[i] != '.')
                        return false;
            }

            var normalized = System.Text.RegularExpressions.Regex.Replace(pattern, @"\.+$", "");
            var endsWithDot = normalized.Length != pattern.Length;

            var endWeight = 0;
            if (endsWithDot)
            {
                var lastNonWildcard = normalized.Length - 1;
                for (; lastNonWildcard >= 0; lastNonWildcard--)
                {
                    var c = normalized[lastNonWildcard];
                    if (c == '*')
                        endWeight += short.MaxValue;
                    else if (c == '?')
                        endWeight += 1;
                    else
                        break;
                }

                if (endWeight > 0)
                    normalized = normalized.Substring(0, lastNonWildcard + 1);
            }

            var endsWithWildcardDot = endWeight > 0;
            var endsWithDotWildcardDot = endsWithWildcardDot && normalized.EndsWith(".");
            if (endsWithDotWildcardDot)
                normalized = normalized.Substring(0, normalized.Length - 1);

            normalized = System.Text.RegularExpressions.Regex.Replace(normalized, @"(?!^)(\.\*)+$", @".*");

            var escaped = System.Text.RegularExpressions.Regex.Escape(normalized);
            string head, tail;

            if (endsWithDotWildcardDot)
            {
                head = "^" + escaped;
                tail = @"(\.[^.]{0," + endWeight + "})?$";
            }
            else if (endsWithWildcardDot)
            {
                head = "^" + escaped;
                tail = "[^.]{0," + endWeight + "}$";
            }
            else
            {
                head = "^" + escaped;
                tail = "$";
            }

            if (head.EndsWith(@"\.\*") && head.Length > 5)
            {
                head = head.Substring(0, head.Length - 4);
                tail = @"(\..*)?" + tail;
            }

            var regex = head.Replace(@"\*", ".*").Replace(@"\?", "[^.]?") + tail;
            return System.Text.RegularExpressions.Regex.IsMatch(value, regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        #endregion MatchesWildcardWindowsStyle

        #region MatchesWildcardUnixStyle

        /// <summary>
        /// Checks using Unix style, if value matches the specified wildcard pattern, i.e. "*.jpg".
        /// <locDE><para />Prüft nach Unix-Art, ob der Wert einer der angegebenen Wildcard-Maske entspricht, z.B. "*.jpg".</locDE>
        /// </summary>
        /// <param name="value">The value to check if it matches against the specified wildcard pattern.
        /// <locDE><para />Der Wert, der gegen die angegebenen Wildcard-Maske geprüft werden soll.</locDE></param>
        /// <param name="pattern">The pattern, i.e. "*.jpg".<locDE><para />Die Maske, z.B. "*.jpg".</locDE></param>
        /// <returns>True if value matches any wildcard pattern, otherwise false.
        /// <locDE><para />True, falls der Wert der Wildcard-Maske entspricht, sonst false.</locDE></returns>
        private static bool MatchesWildcardUnixStyle(string value, string pattern)
        {
            // http://stackoverflow.com/a/16488364
            var regex = "^" + System.Text.RegularExpressions.Regex.Escape(pattern)
                                    .Replace("\\*", ".*")
                                    .Replace("\\?", ".")
                        + "$";

            return System.Text.RegularExpressions.Regex.IsMatch(value, regex);
        }

        #endregion MatchesWildcardUnixStyle

        #region ExtractNumber

        /// <summary>
        /// Extracts a block of digits as number. Returns true if number was found.
        /// <locDE><para />Extrahiert einen Ziffernblock als Zahl. Liefert true falls eine Zahl gefunden wurde.</locDE>
        /// </summary>
        /// <param name="value">The value to parse a digit block from.<locDE><para />Der Wert, aus dem ein Ziffernblock extrahiert werden soll.</locDE></param>
        /// <param name="foundNumber">The found number.<locDE><para />Die gefundene Zahl.</locDE></param>
        /// <param name="removeFound">Should the found number be removed?<locDE><para />Soll die gefundene Nummer entfernt werden?</locDE></param>
        /// <param name="trimStart">Should the value be trimmed after number was removed?<locDE><para />Soll der Wert nach der Entfernung getrimmt werden?</locDE></param>
        /// <returns>True if number was found (and removed).<locDE><para />True, falls eine Zahl gefunden (und entfernt) wurde.</locDE></returns>
        public static bool ExtractNumber(ref string value, out int foundNumber, bool removeFound = true, bool trimStart = true)
        {
            int foundDigits;
            return ExtractNumber(ref value, out foundNumber, out foundDigits, removeFound, trimStart);
        }

        /// <summary>
        /// Extracts a block of digits as number. Returns true if number was found.
        /// <locDE><para />Extrahiert einen Ziffernblock als Zahl. Liefert true falls eine Zahl gefunden wurde.</locDE>
        /// </summary>
        /// <param name="value">The value to parse a digit block from.<locDE><para />Der Wert, aus dem ein Ziffernblock extrahiert werden soll.</locDE></param>
        /// <param name="foundNumber">The found number.<locDE><para />Die gefundene Zahl.</locDE></param>
        /// <param name="foundDigits">The number of found digits (negative sign does not count as digit!).<locDE><para />Die Anzahl der gefundenen Ziffern (Minus zählt nicht als Ziffer!).</locDE></param>
        /// <param name="removeFound">Should the found number be removed?<locDE><para />Soll die gefundene Nummer entfernt werden?</locDE></param>
        /// <param name="trimStart">Should the value be trimmed after number was removed?<locDE><para />Soll der Wert nach der Entfernung getrimmt werden?</locDE></param>
        /// <returns>True if number was found (and removed).<locDE><para />True, falls eine Zahl gefunden (und entfernt) wurde.</locDE></returns>
        public static bool ExtractNumber(ref string value, out int foundNumber, out int foundDigits, bool removeFound = true, bool trimStart = true)
        {
            foundNumber = -1;
            foundDigits = 0;

            StringBuilder digits = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (0 == digits.Length && '-' == value[i])
                {
                    // Leading negative sign
                    digits.Append(value[i]);
                }
                else if (Char.IsDigit(value[i]))
                {
                    digits.Append(value[i]);
                    foundDigits++;
                }
                else
                    break;
            }

            if (removeFound)
            {
                value = value.Substring(digits.Length);
                if (trimStart)
                    value = value.TrimStart();
            }

            if (digits.Length > 0)
                return int.TryParse(digits.ToString(), out foundNumber);
            return false;
        }

        #endregion ExtractNumber

        #region CountOccurrence

        /// <summary>
        /// Gets the maximum occurrence count of any of the given chars.
        /// <locDE><para />Liefert die maximale Vorkommensanzahl eines der angegebenen Zeichen.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="chars">The chars to count the occurence for.
        /// <locDE><para />Die Zeichen, deren Vorkommensanzahl ermittelt werden soll.</locDE></param>
        /// <returns>Maximum occurrence count of any of the given chars.
        /// <locDE><para />Maximale Vorkommensanzahl eines der angegebenen Zeichen.</locDE>
        /// </returns>
        public static int CountOccurrence(this string value, char[] chars)
        {
            int max = 0;

            foreach (char ch in chars)
            {
                int found = CountOccurrence(value, ch);
                if (found > max)
                    max = found;
            }
            return max;
        }

        /// <summary>
        /// Gets the maximum occurrence count of the given char.
        /// <locDE><para />Liefert die maximale Vorkommensanzahl des angegebenen Zeichens.</locDE>
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="ch">The char to count the occurence for.
        /// <locDE><para />Das Zeichen, dessen Vorkommensanzahl ermittelt werden soll.</locDE></param>
        /// <returns>Maximum occurrence count of the given char.
        /// <locDE><para />Maximale Vorkommensanzahl des angegebenen Zeichens.</locDE>
        /// </returns>
        public static int CountOccurrence(this string value, char ch)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            return value.Split(ch).Length - 1;
        }

        #endregion CountOccurrence

        #region GetBetween

        /// <summary>
        /// Gets a substring surrounded by given delimiters within source string.
        /// Returns null if invalid syntax, i.e. at least on delimiter was not found, etc.
        /// <locDE><para />Ermittelt den Teilstring, der zwischen den Klammern steht.
        /// Liefert Null bei Syntaxfehler, z.B. mind. ein Trenner nicht gefunden, etc.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="startDelimiter">The start/front delimiter.<locDE><para />Der davorstehende Trenner.</locDE></param>
        /// <param name="endDelimiter">The end/back delimiter.<locDE><para />Der nachfolgende Trenner.</locDE></param>
        /// <returns>Null if invalid syntax, i.e. at least on delimiter was not found, etc.
        /// <locDE><para />Null bei Syntaxfehler, z.B. mind. ein Trenner nicht gefunden, etc.</locDE>
        /// </returns>
        public static string GetBetween(this string value, string startDelimiter, string endDelimiter)
        {
            return GetSubstringInsideOf(startDelimiter, endDelimiter, value);
        }

        #endregion GetBetween

        #region GetSubstringInsideOf

        /// <summary>
        /// Gets a substring surrounded by given brackets within source string and leaves <paramref name="source"/> untouched.
        /// Returns null if invalid syntax, i.e. opening bracket without closing, etc.
        /// <locDE><para />Ermittelt den Teilstring, der zwischen den Klammern steht, und ändert <paramref name="source"/> nicht.
        /// Liefert Null bei Syntaxfehler, z.B. öffnende Klammer ohne schließende, etc.</locDE>
        /// </summary>
        /// <param name="openBracket">The opening bracket.<locDE><para />Die öffnende Klammer.</locDE></param>
        /// <param name="closeBracket">The closing bracket.<locDE><para />Die schließende Klammer.</locDE></param>
        /// <param name="source">The source string.<locDE><para />Der Quellstring.</locDE></param>
        /// <returns>Null if invalid syntax, i.e. opening bracket without closing, etc.
        /// <locDE><para />Null bei Syntaxfehler, z.B. öffnende Klammer ohne schließende, etc.</locDE>
        /// </returns>
        public static string GetSubstringInsideOf(string openBracket, string closeBracket, string source)
        {
            if (null == source || string.IsNullOrEmpty(openBracket) || string.IsNullOrEmpty(closeBracket))
                return null;

            // ??? Maybe improve this to handle multi-level surroundings, i.e. "{xx{yy}zz}"
            // Count number of opening brackets within the found value and look for as many closing brackets afterwards

            int idxOpen = source.IndexOf(openBracket);
            int idxClose = source.IndexOf(closeBracket, (-1 != idxOpen) ? (idxOpen + openBracket.Length) : 0);
            if (-1 == idxOpen || -1 == idxClose)
                return null;

            string valueWithinBrackets = source.Substring((idxOpen + openBracket.Length), (idxClose - idxOpen - openBracket.Length));
            //source = source.Substring(0, idxOpen) + source.Substring(idxClose + closeBracket.Length);
            return valueWithinBrackets;
        }

        /// <summary>
        /// Gets a substring surrounded by given brackets within source string and removes it from <paramref name="source"/>.
        /// Returns null if invalid syntax, i.e. opening bracket without closing, etc.
        /// <locDE><para />Ermittelt den Teilstring, der zwischen den Klammern steht, und entfernt diesen aus <paramref name="source"/>.
        /// Liefert Null bei Syntaxfehler, z.B. öffnende Klammer ohne schließende, etc.</locDE>
        /// </summary>
        /// <param name="openBracket">The opening bracket.<locDE><para />Die öffnende Klammer.</locDE></param>
        /// <param name="closeBracket">The closing bracket.<locDE><para />Die schließende Klammer.</locDE></param>
        /// <param name="source">The source string.<locDE><para />Der Quellstring.</locDE></param>
        /// <returns>Null if invalid syntax, i.e. opening bracket without closing, etc.
        /// <locDE><para />Null bei Syntaxfehler, z.B. öffnende Klammer ohne schließende, etc.</locDE>
        /// </returns>
        public static string GetSubstringInsideOf(string openBracket, string closeBracket, ref string source)
        {
            if (null == source || string.IsNullOrEmpty(openBracket) || string.IsNullOrEmpty(closeBracket))
                return null;

            // ??? Maybe improve this to handle multi-level surroundings, i.e. "{xx{yy}zz}"
            // Count number of opening brackets within the found value and look for as many closing brackets afterwards

            int idxOpen = source.IndexOf(openBracket);
            int idxClose = source.IndexOf(closeBracket, (-1 != idxOpen) ? (idxOpen + openBracket.Length) : 0);
            if (-1 == idxOpen || -1 == idxClose)
                return null;

            string valueWithinBrackets = source.Substring((idxOpen + openBracket.Length), (idxClose - idxOpen - openBracket.Length));
            source = source.Substring(0, idxOpen) + source.Substring(idxClose + closeBracket.Length);
            return valueWithinBrackets;
        }

        #endregion GetSubstringInsideOf

        #region SplitRespectingQuotes

        /// <summary>
        /// Splits the string and takes care of quotes (single and/or double quotes). I.e. 1;"2;3" delivers two entries "1" and "2;3".
        /// <locDE><para />Zerlegt den String und beachtet Anführungszeichen (einfache und/oder doppelte). Z.B. 1;"2;3" ergibt zwei Einträge "1" und "2;3".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="delimiter">The delimiter character.<locDE><para />Das Trennzeichen.</locDE></param>
        /// <param name="removeQuotes">Should quotes be removed?<locDE><para />Sollen Anführungszeichen entfernt werden?</locDE></param>
        /// <returns>String array conatining split results.<locDE><para />String Array mit den zerlegten Teilen.</locDE></returns>
        public static string[] SplitRespectingQuotes(this string value, char delimiter = ';', bool removeQuotes = true)
        {
            return SplitRespectingQuotes(value, StringSplitOptions.None, delimiter, removeQuotes);
        }

        /// <summary>
        /// Splits the string and takes care of quotes (single and/or double quotes). I.e. 1;"2;3" delivers two entries "1" and "2;3".
        /// <locDE><para />Zerlegt den String und beachtet Anführungszeichen (einfache und/oder doppelte). Z.B. 1;"2;3" ergibt zwei Einträge "1" und "2;3".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="delimiters">The delimiter characters.<locDE><para />Die Trennzeichen.</locDE></param>
        /// <param name="removeQuotes">Should quotes be removed?<locDE><para />Sollen Anführungszeichen entfernt werden?</locDE></param>
        /// <returns>String array conatining split results.<locDE><para />String Array mit den zerlegten Teilen.</locDE></returns>
        public static string[] SplitRespectingQuotes(this string value, char[] delimiters, bool removeQuotes = true)
        {
            return SplitRespectingQuotes(value, StringSplitOptions.None, delimiters, removeQuotes);
        }

        /// <summary>
        /// Splits the string and takes care of quotes (single and/or double quotes). I.e. 1;"2;3" delivers two entries "1" and "2;3".
        /// <locDE><para />Zerlegt den String und beachtet Anführungszeichen (einfache und/oder doppelte). Z.B. 1;"2;3" ergibt zwei Einträge "1" und "2;3".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="options">The options, i.e. StringSplitOptions.RemoveEmptyEntries to omit empty entries.
        /// <locDE><para />Die Optionen, z.B. StringSplitOptions.RemoveEmptyEntries um Leereinträge zu verwerfen.</locDE></param>
        /// <param name="delimiter">The delimiter character.<locDE><para />Das Trennzeichen.</locDE></param>
        /// <param name="removeQuotes">Should quotes be removed?<locDE><para />Sollen Anführungszeichen entfernt werden?</locDE></param>
        /// <returns>String array conatining split results.<locDE><para />String Array mit den zerlegten Teilen.</locDE></returns>
        public static string[] SplitRespectingQuotes(this string value, StringSplitOptions options, char delimiter = ';', bool removeQuotes = true)
        {
            return SplitRespectingQuotes(value, options, new char[] { delimiter }, removeQuotes);
        }

        /// <summary>
        /// Splits the string and takes care of quotes (single and/or double quotes). I.e. 1;"2;3" delivers two entries "1" and "2;3".
        /// <locDE><para />Zerlegt den String und beachtet Anführungszeichen (einfache und/oder doppelte). Z.B. 1;"2;3" ergibt zwei Einträge "1" und "2;3".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="options">The options, i.e. StringSplitOptions.RemoveEmptyEntries to omit empty entries.
        /// <locDE><para />Die Optionen, z.B. StringSplitOptions.RemoveEmptyEntries um Leereinträge zu verwerfen.</locDE></param>
        /// <param name="delimiters">The delimiter characters.<locDE><para />Die Trennzeichen.</locDE></param>
        /// <param name="removeQuotes">Should quotes be removed?<locDE><para />Sollen Anführungszeichen entfernt werden?</locDE></param>
        /// <returns>String array conatining split results.<locDE><para />String Array mit den zerlegten Teilen.</locDE></returns>
        public static string[] SplitRespectingQuotes(this string value, StringSplitOptions options, char[] delimiters, bool removeQuotes = true)
        {
            if (null == value)
                return null;

            // http://stackoverflow.com/a/31804981

            char[] characters = value.ToCharArray();
            System.Collections.Generic.List<string> returnValueList = new System.Collections.Generic.List<string>();
            StringBuilder tempString = new StringBuilder();
            bool blockUntilEndQuote = false;
            bool blockUntilEndQuote2 = false;
            int characterCount = 0;

            foreach (char ch in characters)
            {
                characterCount = characterCount + 1;

                if (ch == '"' && !blockUntilEndQuote2)
                {
                    if (!blockUntilEndQuote)
                    {
                        blockUntilEndQuote = true;
                    }
                    else if (blockUntilEndQuote)
                    {
                        blockUntilEndQuote = false;
                    }
                }
                if (ch == '\'' && !blockUntilEndQuote)
                {
                    if (!blockUntilEndQuote2)
                    {
                        blockUntilEndQuote2 = true;
                    }
                    else if (blockUntilEndQuote2)
                    {
                        blockUntilEndQuote2 = false;
                    }
                }

                if (!delimiters.Contains(ch))
                {
                    tempString.Append(ch);
                }
                else if (delimiters.Contains(ch) && (blockUntilEndQuote || blockUntilEndQuote2))
                {
                    tempString.Append(ch);
                }
                else
                {
                    if (removeQuotes &&
                        tempString.Length >= 2 &&
                        ('\'' == tempString[0] || '"' == tempString[0]))
                    {
                        // Remove surrounding quotes
                        //tempString = tempString.Mid(1, tempString.Length - 2);
                        tempString.Remove(0, 1);
                        tempString.Remove(tempString.Length - 1, 1);
                    }

                    if (StringSplitOptions.RemoveEmptyEntries != options || 0 != tempString.Length)
                        returnValueList.Add(tempString.ToString());

                    tempString.Clear();
                }

                if (characterCount == characters.Length)
                {
                    if (removeQuotes &&
                        tempString.Length >= 2 &&
                        ('\'' == tempString[0] || '"' == tempString[0]))
                    {
                        // Remove surrounding quotes
                        //tempString = tempString.Mid(1, tempString.Length - 2);
                        tempString.Remove(0, 1);
                        tempString.Remove(tempString.Length - 1, 1);
                    }

                    if (StringSplitOptions.RemoveEmptyEntries != options || 0 != tempString.Length)
                        returnValueList.Add(tempString.ToString());

                    tempString.Clear();
                }
            }

            string[] returnValue = returnValueList.ToArray();
            return returnValue;
        }

        #endregion SplitRespectingQuotes

        #region GetPartsWithMaxLength

        /// <summary>
        /// Splits the specified string into parts with specified maximum length.
        /// <locDE><para />Zerlegt den angegebenen String in Teilstrings mit der angegebenen Maximallänge.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="maxLength">The maximum length of each part.<locDE><para />Die Maximallänge der Teilstrings.</locDE></param>
        /// <returns>List of string parts.<locDE><para />Liste der Teilstrings.</locDE></returns>
        public static System.Collections.Generic.IEnumerable<string> GetPartsWithMaxLength(this string value, int maxLength)
        {
            if (null == value)
                yield break;

            for (int i = 0; i < value.Length; i += maxLength)
                yield return value.Substring(i, Math.Min(maxLength, value.Length - i));
        }

        #endregion GetPartsWithMaxLength

        #region GetFirstLine

        /// <summary>
        /// Gets the first line of a (possibly) multi-line text (empty string if null).
        /// <locDE><para />Liefert die erste Zeile eines (möglicherweise) mehrzeiligen Texts (Leerstring wenn Null).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>First line of a (possibly) multi-line text (empty string if null).
        /// <locDE><para />Erste Zeile eines (möglicherweise) mehrzeiligen Texts (Leerstring wenn Null).</locDE>
        /// </returns>
        public static string GetFirstLine(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            int index = value.IndexOfAny(new char[] { '\r', '\n' });
            return (-1 == index) ? value : value.Left(index);
        }

        #endregion GetFirstLine

        #region InsertBetweenDigitAndLetter

        /// <summary>
        /// Inserts a string (i.e. space) between digit/dot/comma(s) and letter(s), i.e. "23min" --> "23 min".
        /// <locDE><para />Fügt einen String (z.B. Leerzeichen) zwischen Ziffer(n) und Buchstabe(n) ein, z.B. "23min" --> "23 min".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="insertValue">The insert value, i.e. space: " ".<locDE><para />Der einzufügende String, z.B. Leerzeichen: " ".</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string InsertBetweenDigitAndLetter(this string value, string insertValue)
        {
            if (null == value || null == insertValue)
                return null;

            bool lastWasDigit = false;
            StringBuilder work = new StringBuilder();
            foreach (char ch in value)
            {
                if (char.IsLetter(ch) && lastWasDigit)
                    work.Append(insertValue);

                work.Append(ch);
                lastWasDigit = char.IsDigit(ch) || '.' == ch || ',' == ch;
            }
            return work.ToString();
        }

        #endregion InsertBetweenDigitAndLetter

        #region Left

        /// <summary>
        /// Gets the specified number of left/first characters (empty string if null).
        /// <locDE><para />Liefert die angegebene Anzahl Zeichen von links/Stringanfang (Leerstring wenn Null).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="length">The number of characters.<locDE><para />Die gewünschte Anzahl Zeichen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (length < 0)
                length = 0;

            return value.Substring(0, Math.Min(length, value.Length));
        }

        #endregion Left

        #region LeftOf

        /// <summary>
        /// Gets the left/first characters until the stop character(s) appear (empty string if null).
        /// <locDE><para />Liefert die Zeichen von links/Stringanfang bis zur Stopp-Zeichenfolge (Leerstring wenn Null).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="delimiter">The character(s) to stop at when found.<locDE><para />Die Zeichenfolge, bei der gestoppt werden soll.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string LeftOf(this string value, string delimiter)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                int charLocation = value.IndexOf(delimiter, StringComparison.OrdinalIgnoreCase);

                if (charLocation > 0)
                {
                    return value.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

        #endregion LeftOf

        #region Right

        /// <summary>
        /// Gets the specified number of right/last characters (empty string if null).
        /// <locDE><para />Liefert die angegebene Anzahl Zeichen von rechts/Stringende (Leerstring wenn Null).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="length">The number of characters.<locDE><para />Die gewünschte Anzahl Zeichen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (length < 0)
                length = 0;

            return (value.Length >= length) ? value.Substring(value.Length - length, length) : value;
        }

        #endregion Right

        #region RightOf

        /// <summary>
        /// Gets the characters beginning at where the start character(s) appear until the string end (empty string if null).
        /// If the start character(s) appear multiple times, the last occurrence is taken.
        /// <locDE><para />Liefert die Zeichen ab der Start-Zeichenfolge bis zum Stringende (Leerstring wenn Null).
        /// Falls die Start-Zeichenfolge mehrmals enthalten ist, wird die letzte Fundstelle gewählt.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="delimiter">The character(s) to start at (if found).<locDE><para />Die Zeichenfolge, nach der begonnen werden soll.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string RightOf(this string value, string delimiter)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                int charLocation = value.LastIndexOf(delimiter, StringComparison.OrdinalIgnoreCase);

                if (charLocation > 0)
                {
                    return value.Substring(charLocation + delimiter.Length);
                }
            }

            return string.Empty;
        }

        #endregion RightOf

        #region Mid

        /// <summary>
        /// Gets the specified number of middle (in between) characters (empty string if null).
        /// <locDE><para />Liefert die angegebene Anzahl Zeichen aus der Stringmitte (Leerstring wenn Null).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="length">The number of characters (-1 means all available characters).
        /// <locDE><para />Die gewünschte Anzahl Zeichen (-1 heißt alle vorhandenen Zeichen).</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string Mid(this string value, int startIndex, int length = -1)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (startIndex < 0)
                startIndex = 0;
            if (length < 0)
                length = int.MaxValue;

            if (startIndex >= value.Length)
                return string.Empty;

            if (int.MaxValue == length || (startIndex + length) > value.Length)
                length = value.Length - startIndex;

            if (length <= 0)
                return string.Empty;

            return value.Substring(startIndex, length);
        }

        #endregion Mid

        #region Truncate(At)

        /// <summary>
        /// Truncates the string after maximum numbers of chars to keep. Trims the end by default.
        /// <locDE><para />Schneidet den String nach der Maximalanzahl Zeichen ab. Trimmt das Stringende standardmäßig.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="maxCharsToKeep">The number of chars to keep (truncate the rest).<locDE><para />Die Anzahl zu behaltender Zeichen (den Rest abschneiden).</locDE></param>
        /// <param name="trimEnd">Should the string end be trimmed?<locDE><para />Soll das Stringende getrimmt werden?</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string Truncate(this string value, int maxCharsToKeep, bool trimEnd = true)
        {
            if (string.IsNullOrEmpty(value) || maxCharsToKeep <= 0)
                return string.Empty;

            if (value.Length > maxCharsToKeep)
            {
                // Value is longer than desired, truncate
                string result = value.Substring(0, maxCharsToKeep);
                if (trimEnd)
                    return result.TrimEnd();
                return result;
            }

            // Value is shorter than (or equal to) truncate length
            if (trimEnd)
                return value.TrimEnd();
            return value;
        }

        /// <summary>
        /// Truncates value at (before) <paramref name="marker"/>, returns truncated value (or untouched value if <paramref name="marker"/> not found). Trims the end by default.
        /// <locDE><para />Schneidet den String bei/vor <paramref name="marker"/>, liefert gekürzten Wert (oder unveränderten Wert, falls <paramref name="marker"/> nicht vorhanden). Trimmt das Stringende standardmäßig.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="marker">The marker to find.<locDE><para />Der zu findende Marker-Ausdruck.</locDE></param>
        /// <param name="trimEnd">Should the string end be trimmed?<locDE><para />Soll das Stringende getrimmt werden?</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string Truncate(this string value, string marker, bool trimEnd = true)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (string.IsNullOrEmpty(marker))
                return value;

            int idxMarker = value.IndexOf(marker);
            if (-1 == idxMarker)
            {
                // Marker not found, nowhere to truncate
                if (trimEnd)
                    return value.TrimEnd();
                return value;
            }

            // Truncate just before the found marker (remove marker from result)
            return value.Truncate(idxMarker, trimEnd);
        }

        /// <summary>
        /// Truncates value at (before) <paramref name="marker"/>, returns truncated value (or untouched value if <paramref name="marker"/> not found). Always trims the end.
        /// <locDE><para />Schneidet den String bei/vor <paramref name="marker"/>, liefert gekürzten Wert (oder unveränderten Wert, falls <paramref name="marker"/> nicht vorhanden). Trimmt das Stringende.</locDE>
        /// </summary>
        /// <param name="marker">The marker to find.<locDE><para />Der zu findende Marker-Ausdruck.</locDE></param>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        [Obsolete("TruncateAt is deprecated, please use string extension method Truncate instead.   TruncateAt wird aufgelassen, bitte stattdessen String Extension Methode Truncate verwenden.")]
        public static string TruncateAt(string marker, string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (string.IsNullOrEmpty(marker))
                return value;

            int idxMarker = value.IndexOf(marker);
            if (-1 == idxMarker)
                return value.TrimEnd();

            string result;
            result = value.Remove(idxMarker).TrimEnd();
            return result;
        }

        #endregion Truncate(At)

        #region TrimStartLetters

        /// <summary>
        /// Trims/removes the letters at start of string.
        /// <locDE><para />Trimmt/entfernt die Buchstaben am Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartLetters(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsLetter(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartLetters

        #region TrimStartNonLetters

        /// <summary>
        /// Trims the non letter characters at start of string.
        /// <locDE><para />Trimmt/entfernt alles ausser Buchstaben am Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartNonLetters(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsLetter(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartNonLetters

        #region TrimStartDigits

        /// <summary>
        /// Trims the digits at start of string.
        /// <locDE><para />Trimmt/entfernt Ziffern am Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartDigits(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsDigit(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartDigits

        #region TrimStartNonDigits

        /// <summary>
        /// Trims the non digit characters at start of string.
        /// <locDE><para />Trimmt/entfernt alles ausser Ziffern am Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartNonDigits(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsDigit(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartNonDigits

        #region TrimStartAlphaNum

        /// <summary>
        /// Trims the alphanumeric characters at start of string.
        /// <locDE><para />Trimmt/entfernt Buchstaben/Ziffern am Stringanfang.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartAlphaNum(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsLetterOrDigit(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartAlphaNum

        #region TrimStartNonAlphaNum

        /// <summary>
        /// Trims the non alphanumeric characters at start of string, i.e. "-/.,; " etc.
        /// <locDE><para />Trimmt/entfernt alles ausser Buchstaben/Ziffern am Stringanfang, z.B. "-/.,; " etc.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimStartNonAlphaNum(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsLetterOrDigit(value[0]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[0].ToString()))))
            {
                value = value.Substring(1);
            }
            return value;
        }

        #endregion TrimStartNonAlphaNum

        #region TrimEndLetters

        /// <summary>
        /// Trims/removes the letters at end of string.
        /// <locDE><para />Trimmt/entfernt die Buchstaben am Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndLetters(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsLetter(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndLetters

        #region TrimEndNonLetters

        /// <summary>
        /// Trims the non letter characters at end of string.
        /// <locDE><para />Trimmt/entfernt alles ausser Buchstaben am Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndNonLetters(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsLetter(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndNonLetters

        #region TrimEndDigits

        /// <summary>
        /// Trims the digits at end of string.
        /// <locDE><para />Trimmt/entfernt die Ziffern am Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndDigits(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsDigit(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndDigits

        #region TrimEndNonDigits

        /// <summary>
        /// Trims the non digit characters at end of string.
        /// <locDE><para />Trimmt/entfernt alles ausser Ziffern am Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndNonDigits(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsDigit(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndNonDigits

        #region TrimEndAlphaNum

        /// <summary>
        /// Trims the alphanumeric characters at end of string.
        /// <locDE><para />Trimmt/entfernt die Buchstaben/Ziffern am Stringende.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndAlphaNum(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (char.IsLetterOrDigit(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndAlphaNum

        #region TrimEndNonAlphaNum

        /// <summary>
        /// Trims the non alphanumeric characters at end of string, i.e. "-/.,; " etc.
        /// <locDE><para />Trimmt/entfernt alles ausser Buchstaben/Ziffern am Stringende, z.B. "-/.,; " etc.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="moreCharsToTrim">The further chars to trim/remove.<locDE><para />Die weiteren Zeichen zu trimmen/entfernen.</locDE></param>
        /// <returns>Processed string.<locDE><para />Bearbeiteten String.</locDE></returns>
        public static string TrimEndNonAlphaNum(this string value, string moreCharsToTrim = null)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            while (value.Length > 0 &&
                (!char.IsLetterOrDigit(value[value.Length - 1]) || (null != moreCharsToTrim && moreCharsToTrim.Contains(value[value.Length - 1].ToString()))))
            {
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }

        #endregion TrimEndNonAlphaNum

        #region TrimEntries

        /// <summary>
        /// Trims all string items of a string array.
        /// <locDE><para />Trimmt alle Strings eines String Arrays.</locDE>
        /// </summary>
        /// <param name="items">The items.<locDE><para />Die Elemente.</locDE></param>
        /// <param name="charsToTrim">The (non-default) chars to trim.<locDE><para />Die (nicht standardmäßigen) zu trimmenden Zeichen.</locDE></param>
        /// <returns>Processed string array.<locDE><para />Bearbeiteten String Array.</locDE></returns>
        public static string[] TrimEntries(this string[] items, char[] charsToTrim = null)
        {
            if (null == items)
                return null;

            int idx = 0;
            string[] newItems = new string[items.Length];
            foreach (string item in items)
            {
                string newItem;
                if (null != charsToTrim)
                    newItem = item.Trim(charsToTrim);
                else
                    newItem = item.Trim();

                newItems[idx] = newItem;
                idx++;
            }
            return newItems;
        }

        #endregion TrimEntries

        #region GetDefaultEncoding

        private static Encoding _DefaultEncoding = null;

        /// <summary>
        /// Gets the default encoding (Windows-1252).
        /// <locDE><para />Liefert das Standard-Encoding (Windows-1252).</locDE>
        /// </summary>
        /// <returns>Default encoding (Windows-1252).<locDE><para />Standard-Encoding (Windows-1252).</locDE></returns>
        public static Encoding GetDefaultEncoding()
        {
            if (null == _DefaultEncoding)
            {
                Encoding enc = null;
                //try { if (null == enc) enc = UnicodeEncoding.Default; } catch { }
                try { if (null == enc) enc = UnicodeEncoding.GetEncoding("Windows-1252"); } catch { }   // Use native "Windows-1252" from .NET FW (OS)
                try { if (null == enc) enc = new EncodingWindows1252(); } catch { }                     // Hardcoded "Windows-1252" (i.e. for WP8 Silverlight)
                try { if (null == enc) enc = UnicodeEncoding.GetEncoding("iso-8859-1"); } catch { }     // Should never need this
                _DefaultEncoding = enc;
            }
            return _DefaultEncoding;
        }

        #endregion GetDefaultEncoding

        #region ExtractStringContent

        /// <summary>
        /// Extracts a string from a byte array (until first 0x00/0xFF byte or end of array).
        /// <locDE><para />Extrahiert einen String aus einem Byte Array (bis zum ersten 0x00/0xFF byte oder Array-Ende).</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <returns></returns>
        public static string ExtractStringContent(byte[] buffer)
        {
            return ExtractStringContent(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Extracts a string from a byte array (until first 0x00/0xFF byte or end of array).
        /// <locDE><para />Extrahiert einen String aus einem Byte Array (bis zum ersten 0x00/0xFF byte oder Array-Ende).</locDE>
        /// </summary>
        /// <param name="buffer">The buffer.<locDE><para />Der Buffer.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="count">The count (-1 = all).<locDE><para />Die gewünschte Anzahl (-1 = alles).</locDE></param>
        /// <returns></returns>
        public static string ExtractStringContent(byte[] buffer, int startIndex, int count)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (null == buffer || startIndex >= buffer.Length)
                return string.Empty;
            if (count < 0)
                count = buffer.Length - startIndex;

            // Convert 0xFF to zero (terminator); after first 0 everything is 0 by convention
            // Konvertiere 0xFF zu Null(-Terminator); nach der ersten 0 ist alles 0 (per Konvention)
            int i;
            bool bZeroFound = false;
            for (i = startIndex; i < (startIndex + count); i++)
            {
                if (bZeroFound)
                {
                    buffer[i] = 0;
                    continue;
                }
                if (0xFF == buffer[i])
                    buffer[i] = 0;
                if (0 == buffer[i])
                    bZeroFound = true;
            }

            // Find the last valid (non-zero) byte
            // Finde das letzte gültige (nicht-Null) Byte
            i = count - 1;
            while (i >= 0 && buffer[startIndex + i] == 0)
                --i;

            // i is the index of the last non-zero byte --> index < 0 means: empty string
            // i ist der Index des letzten nicht-Null Bytes --> index < 0 heißt: Leerstring
            if (i < 0)
                return String.Empty;

            // Now response[i] is the last non-zero byte
            // response[i] ist jetzt das letzte nicht-Null Byte
            byte[] work = new byte[i + 1];
            Array.Copy(buffer, startIndex, work, 0, i + 1);

            //return UnicodeEncoding.Default.GetString(buffer);
            return GetDefaultEncoding().GetString(work, 0, i + 1);
        }

        #endregion ExtractStringContent

        #region StringToByteArray

        /// <summary>
        /// Converts the string contents into a byte array.
        /// <locDE><para />Konvertiert den Stringinhalt in einen Byte Array.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Byte array holding string contents.<locDE><para />Byte Array, welcher die Stringinhalte enthält.</locDE></returns>
        public static byte[] StringToByteArray(string value)
        {
            return StringToByteArray(value, 0, value.Length);
        }

        /// <summary>
        /// Converts the string contents into a byte array.
        /// <locDE><para />Konvertiert den Stringinhalt in einen Byte Array.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="startIndex">The start index (0..n).<locDE><para />Der Startindex (0..n).</locDE></param>
        /// <param name="count">The count (-1 = from <paramref name="startIndex"/> to end).
        /// <locDE><para />Die gewünschte Anzahl (-1 = von <paramref name="startIndex"/> bis zum Ende).</locDE></param>
        /// <returns>Byte array holding string contents.<locDE><para />Byte Array, welcher die Stringinhalte enthält.</locDE></returns>
        public static byte[] StringToByteArray(string value, int startIndex, int count)
        {
            if (null == value)
                return null;

            if (startIndex < 0)
                startIndex = 0;

            if (count < 0 || count > (value.Length - startIndex))
                count = value.Length - startIndex;

            char[] szBuffer = new char[count];
            //Array.Copy(value.ToCharArray(startIdx, count), szBuffer, count);
            string part = value.Substring(startIndex, count);
            Array.Copy(part.ToCharArray(), szBuffer, count);
            //return UnicodeEncoding.Default.GetBytes(szBuffer);
            return GetDefaultEncoding().GetBytes(szBuffer);
        }

        #endregion StringToByteArray

        #region AddStringToByteList

        /// <summary>
        /// Adds the string contents to byte list.
        /// <locDE><para />Fügt die Stringinhalte zur Byteliste hinzu.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="list">The byte list.<locDE><para />Die Byteliste.</locDE></param>
        /// <param name="fixedByteCount">The fixed byte count (if not 0, take up to <paramref name="fixedByteCount"/> characters from string and pad with zeroes).
        /// <locDE><para />Die fixierte Byteanzahl (falls nicht 0, höchstens <paramref name="fixedByteCount"/> Zeichen aus String übernehmen bzw. mit Nullen auffüllen).</locDE></param>
        public static void AddStringToByteList(string value, System.Collections.Generic.IList<byte> list, int fixedByteCount = 0)
        {
            if (null == value || null == list)
                return;

            if (fixedByteCount < 0)
                fixedByteCount = 0;

            if (0 != fixedByteCount)
            {
                // Truncate to fixed length
                value = value.Truncate(fixedByteCount);
            }

            byte[] chars = StringToByteArray(value);
            foreach (byte by in chars)
                list.Add(by);

            for (int idx = 0; idx < (fixedByteCount - value.Length); idx++)
                list.Add(0);
        }

        #endregion AddStringToByteList
    }
}