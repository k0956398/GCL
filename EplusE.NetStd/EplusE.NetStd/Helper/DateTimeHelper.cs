using System;
using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Provides DateTime helper methods, i.e. calendar week calculations, etc.
    /// <locDE><para />Stellt DateTime Hilfmethoden bereit, z.B. Kalenderwochenberechnung, etc.</locDE>
    /// </summary>
    public static class DateTimeHelper
    {
        #region DefaultCalendarWeekRule
        private static bool _DefaultCalendarWeekRule_Loaded = false;
        private static System.Globalization.CalendarWeekRule _DefaultCalendarWeekRule = System.Globalization.CalendarWeekRule.FirstFourDayWeek;

        /// <summary>
        /// Gets or sets the calendar week rule default value, i.e. FirstFourDayWeek.
        /// <locDE><para />Holt/setzt die Vorgabe-Kalenderwochenregel, z.B. FirstFourDayWeek (erste 4-Tage-Woche).</locDE>
        /// </summary>
        /// <value>The calendar week rule default value, i.e. FirstFourDayWeek.
        /// <locDE><para />Die Vorgabe-Kalenderwochenregel, z.B. FirstFourDayWeek (erste 4-Tage-Woche).</locDE></value>
        public static System.Globalization.CalendarWeekRule DefaultCalendarWeekRule
        {
            get
            {
                if (!_DefaultCalendarWeekRule_Loaded)
                {
                    try
                    {
                        string value = AppSettings.GetValue("DefaultCalendarWeekRule");
                        if (!string.IsNullOrWhiteSpace(value))
                            _DefaultCalendarWeekRule = (System.Globalization.CalendarWeekRule)Enum.Parse(typeof(System.Globalization.CalendarWeekRule), value, true);
                    }
                    catch { }
                    _DefaultCalendarWeekRule_Loaded = true;
                }
                return _DefaultCalendarWeekRule;
            }

            set
            {
                if (value != _DefaultCalendarWeekRule)
                {
                    _DefaultCalendarWeekRule = value;
                    AppSettings.SetValue("DefaultCalendarWeekRule", value.ToString());
                }
            }
        }
        #endregion
        #region DefaultFirstDayOfWeek
        private static bool _DefaultFirstDayOfWeek_Loaded = false;
        private static System.DayOfWeek _DefaultFirstDayOfWeek = DayOfWeek.Monday;

        /// <summary>
        /// Gets or sets the first day of week default value, i.e. Monday.
        /// <locDE><para />Holt/setzt den Vorgabewert für den ersten Tag der Woche, z.B. Monday (Montag).</locDE>
        /// </summary>
        /// <value>The first day of week default value, i.e. Monday.
        /// <locDE><para />Der Vorgabewert für den ersten Tag der Woche, z.B. Monday (Montag).</locDE></value>
        public static System.DayOfWeek DefaultFirstDayOfWeek
        {
            get
            {
                if (!_DefaultFirstDayOfWeek_Loaded)
                {
                    try
                    {
                        string value = AppSettings.GetValue("DefaultFirstDayOfWeek");
                        if (!string.IsNullOrWhiteSpace(value))
                            _DefaultFirstDayOfWeek = (System.DayOfWeek)Enum.Parse(typeof(System.DayOfWeek), value, true);
                    }
                    catch { }
                    _DefaultFirstDayOfWeek_Loaded = true;
                }
                return _DefaultFirstDayOfWeek;
            }

            set
            {
                if (value != _DefaultFirstDayOfWeek)
                {
                    _DefaultFirstDayOfWeek = value;
                    AppSettings.SetValue("DefaultFirstDayOfWeek", value.ToString());
                }
            }
        }
        #endregion

        #region Constant values
        /// <summary>
        /// Invalid DateTime value: 0001-01-01 00:00:00 UTC.
        /// <locDE><para />Ungültiger DateTime-Wert: 0001-01-01 00:00:00 UTC.</locDE>
        /// </summary>
        public static readonly DateTime InvalidDateTime = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Forever (infinity) DateTime value: 9999-12-31 00:00:00 UTC.
        /// <locDE><para />Unendlicher DateTime-Wert: 9999-12-31 00:00:00 UTC.</locDE>
        /// </summary>
        public static readonly DateTime ForeverDateTime = new DateTime(9999, 12, 31, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Unix/Linux epoch start value: 1970-1-1 00:00:00 UTC.
        /// <locDE><para />Unix/Linux Epochen-Startwert: 1970-1-1 00:00:00 UTC.</locDE>
        /// </summary>
        private static readonly DateTime _StartOfEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region GetEpochTimestamp
        /// <summary>
        /// Gets the epoch timestamp from given DateTime value (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Liefert den Epochen-Zeitstempel für den angegebenen DateTime-Wert (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <param name="dt">The datetime value (UTC or local time).<locDE><para />Der DateTime-Wert (UTC oder Lokalzeit).</locDE></param>
        /// <returns>Epoch timestamp.<locDE><para />Epochen-Zeitstempel.</locDE></returns>
        public static UInt64 GetEpochTimestamp(DateTime dt)
        {
            if (DateTimeKind.Utc == dt.Kind)
                return GetEpochTimestampFromUTC(dt);

            return GetEpochTimestampFromLocalTime(dt);
        }
        #endregion
        #region GetEpochTimestampFromLocalTime
        /// <summary>
        /// Gets the epoch timestamp from given local time value (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Liefert den Epochen-Zeitstempel für die angegebene Lokalzeit (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <param name="dt">The datetime value (local time).<locDE><para />Der DateTime-Wert (Lokalzeit).</locDE></param>
        /// <returns>Epoch timestamp.<locDE><para />Epochen-Zeitstempel.</locDE></returns>
        public static UInt64 GetEpochTimestampFromLocalTime(DateTime dt)
        {
            // Local time --> UTC
            return (UInt64)(dt.ToUniversalTime() - _StartOfEpoch).TotalMilliseconds;
        }
        #endregion
        #region GetEpochTimestampFromUTC
        /// <summary>
        /// Gets the epoch timestamp from given UTC value (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Liefert den Epochen-Zeitstempel für den angegebenen UTC-Wert (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <param name="dt">The datetime value (UTC).<locDE><para />Der DateTime-Wert (UTC).</locDE></param>
        /// <returns>Epoch timestamp.<locDE><para />Epochen-Zeitstempel.</locDE></returns>
        public static UInt64 GetEpochTimestampFromUTC(DateTime dt)
        {
            return (UInt64)(dt - _StartOfEpoch).TotalMilliseconds;
        }
        #endregion
        #region GetDateTimeFromEpochTimestamp
        /// <summary>
        /// Gets DateTime instance (UTC) from a epoch time in milliseconds since 1970-01-01 UTC.
        /// <locDE><para />Liefert einen DateTime-Wert (UTC) vom angegebenen Epochen-Zeitstempel (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <param name="epochTime">The epoch time (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Der Epochen-Zeitstempel (Millisekunden seit 1970-01-01 UTC).</locDE></param>
        /// <returns>DateTime value (UTC).<locDE><para />DateTime-Wert (UTC).</locDE></returns>
        public static DateTime GetDateTimeFromEpochTimestamp(UInt64 epochTime)
        {
            return _StartOfEpoch.AddMilliseconds(epochTime);
        }
        #endregion

        #region Unix time in seconds (32bit value with overflow in the year 2038)
        // http://stackoverflow.com/questions/2883576/how-do-you-convert-epoch-time-in-c

        /// <summary>
        /// Gets DateTime value from a unix time in seconds since 1970-01-01 UTC.
        /// <locDE><para />Liefert DateTime-Wert von Unix-Zeit in Sekunden seit 1970-01-01 UTC.</locDE>
        /// </summary>
        /// <remarks>
        /// 32bit value with overflow in the year 2038!
        /// <locDE><para />32bit-Wert mit Überlauf im Jahr 2038!</locDE>
        /// </remarks>
        /// <param name="unixTime">The unix time (seconds since 1970-01-01 UTC).
        /// <locDE><para />Die Unix-Zeit (Sekunden seit 1970-01-01 UTC).</locDE></param>
        /// <returns>DateTime value.<locDE><para />Der DateTime-Wert.</locDE></returns>
        public static DateTime FromUnixTimeInSeconds(UInt32 unixTime)
        {
            return _StartOfEpoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Gets unix time in seconds since 1970-01-01 UTC from a DateTime value.
        /// <locDE><para />Liefert Unix-Zeit von DateTime-Wert in Sekunden seit 1970-01-01 UTC.</locDE>
        /// </summary>
        /// <remarks>
        /// 32bit value with overflow in the year 2038!
        /// <locDE><para />32bit-Wert mit Überlauf im Jahr 2038!</locDE>
        /// </remarks>
        /// <param name="dt">The datetime value.<locDE><para />Der DateTime-Wert.</locDE></param>
        /// <returns>Unix time (seconds since 1970-01-01 UTC).
        /// <locDE><para />Die Unix-Zeit in Sekunden seit 1970-01-01 UTC.</locDE></returns>
        public static UInt32 ToUnixTimeInSeconds(DateTime dt)
        {
            return (UInt32)(dt.ToUniversalTime() - _StartOfEpoch).TotalSeconds;
        }
        #endregion

        #region IsValidDateTime, IsDateOnly, IsTimeOnly, IsToday, IsYesterday, IsTomorrow, IsSameDay, IsWeekend
        /// <summary>
        /// Determines whether a DateTime value is valid (not 0001-01-01 00:00:00 or year > 9000).
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert gültig ist (nicht 0001-01-01 00:00:00 oder Jahr > 9000).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is valid.<locDE><para />True, falls DateTime-Wert gültig ist.</locDE></returns>
        public static bool IsValidDateTime(this DateTime value)
        {
            if (null != value && value.Equals(InvalidDateTime) || value.Year > 9000)
                return false;
            return true;
        }

        /// <summary>
        /// Determines whether DateTime value contains only a date (all time components are 0).
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert gültig nur einen Datumsanteil enthält (alle Zeitkomponenten gleich 0 sind).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is just a date.<locDE><para />True, falls DateTime-Wert nur einen Datumsanteil enthält.</locDE></returns>
        public static bool IsDateOnly(this DateTime value)
        {
            return (null != value && 0 == value.Hour && 0 == value.Minute && 0 == value.Second && 0 == value.Millisecond);
        }

        /// <summary>
        /// Determines whether DateTime value contains only a time (all date components are 0).
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert gültig nur einen Zeitanteil enthält (alle Datumskomponenten gleich 0 sind).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is just a time.<locDE><para />True, falls DateTime-Wert nur einen Zeitanteil enthält.</locDE></returns>
        public static bool IsTimeOnly(this DateTime value)
        {
            return (null != value && value.Year <= 1 && value.Month <= 1 && value.Day <= 1);
        }

        /// <summary>
        /// Determines whether the DateTime value is of today. Time portion is ignored.
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert "heute" entspricht. Der Zeitanteil wird ignoriert.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is of today.<locDE><para />True, falls DateTime-Wert "heute" entspricht.</locDE></returns>
        public static bool IsToday(this DateTime value)
        {
            return IsSameDay(value, DateTime.Today);
        }

        /// <summary>
        /// Determines whether the DateTime value is of yesterday. Time portion is ignored.
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert "gestern" entspricht. Der Zeitanteil wird ignoriert.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is of yesterday.<locDE><para />True, falls DateTime-Wert "gestern" entspricht.</locDE></returns>
        public static bool IsYesterday(this DateTime value)
        {
            return IsSameDay(value, DateTime.Today.AddDays(-1));
        }

        /// <summary>
        /// Determines whether the DateTime value is of tomorrow. Time portion is ignored.
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert "morgen" entspricht. Der Zeitanteil wird ignoriert.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is of tomorrow.<locDE><para />True, falls DateTime-Wert "morgen" entspricht.</locDE></returns>
        public static bool IsTomorrow(this DateTime value)
        {
            return IsSameDay(value, DateTime.Today.AddDays(1));
        }

        /// <summary>
        /// Determines whether two DateTime values have the same date (= day). Time portion is ignored.
        /// <locDE><para />Ermittelt, ob zwei DateTime-Werte den gleichen Tag beinhalten. Der Zeitanteil wird ignoriert.</locDE>
        /// </summary>
        /// <param name="date1">The date #1.<locDE><para />Der Wert #1.</locDE></param>
        /// <param name="date2">The date #2.<locDE><para />Der Wert #2.</locDE></param>
        /// <returns>True if DateTime values are of the same day.<locDE><para />True, falls DateTime-Werte den gleichen Tag beinhalten.</locDE></returns>
        public static bool IsSameDay(this DateTime date1, DateTime date2)
        {
            return (null != date1 && null != date2 && date1.Year == date2.Year && date1.DayOfYear == date2.DayOfYear);
        }

        /// <summary>
        /// Determines whether the DateTime value is a weekend day (saturday or sunday).
        /// <locDE><para />Ermittelt, ob ein DateTime-Wert auf ein Wochenende fällt (Samstag, Sonntag).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is a weekend day.<locDE><para />True, falls DateTime-Wert auf ein Wochenende fällt.</locDE></returns>
        public static bool IsWeekend(this DateTime value)
        {
            return DayOfWeek.Saturday == value.DayOfWeek || DayOfWeek.Sunday == value.DayOfWeek;
        }
        #endregion

        #region SetKind
        /// <summary>
        /// Sets the DateTime kind (local/UTC) explicitly.
        /// <locDE><para />Setzt die Art (lokal/UTC) eines DateTime-Wertes.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kind">The kind (local or UTC).<locDE><para />Die Art (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value with given kind.<locDE><para />DateTime-Wert mit vorgegebener Art.</locDE></returns>
        public static DateTime SetKind(this DateTime value, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(value, kind);
        }

        /// <summary>
        /// Sets the DateTime kind (local/UTC) explicitly.
        /// <locDE><para />Setzt die Art (lokal/UTC) eines DateTime-Wertes.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kind">The kind (local or UTC).<locDE><para />Die Art (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value with given kind.<locDE><para />DateTime-Wert mit vorgegebener Art.</locDE></returns>
        public static DateTime? SetKind(this DateTime? value, DateTimeKind kind)
        {
            if (!value.HasValue)
                return value;

            return DateTime.SpecifyKind(value.Value, kind);
        }
        #endregion
        #region KindIfUnspecified
        /// <summary>
        /// Sets the DateTime kind (local/UTC) explicitly if currently unspecified.
        /// <locDE><para />Setzt die Art (lokal/UTC) eines DateTime-Wertes, falls aktuell unspezifiziert.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kindIfUnspecified">The kind if unspecified (local or UTC).<locDE><para />Die Art, falls unspezifiziert (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value with valid kind.<locDE><para />DateTime-Wert mit gültiger Art.</locDE></returns>
        public static DateTime KindIfUnspecified(this DateTime value, DateTimeKind kindIfUnspecified)
        {
            if (DateTimeKind.Unspecified == value.Kind)
                return DateTime.SpecifyKind(value, kindIfUnspecified);

            return value;
        }
        #endregion
        #region EnsureLocal
        /// <summary>
        /// Ensures that the DateTime value contains local time (converts if necessary).
        /// <locDE><para />Stellt sicher, dass der DateTime-Wert in Lokalzeit angegeben ist (konvertiert nötigenfalls).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kindIfUnspecified">The kind if unspecified (local or UTC).<locDE><para />Die Art, falls unspezifiziert (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value in local time.<locDE><para />DateTime-Wert in Lokalzeit.</locDE></returns>
        public static DateTime EnsureLocal(this DateTime value, DateTimeKind kindIfUnspecified)
        {
            value = value.KindIfUnspecified(kindIfUnspecified);

            if (DateTimeKind.Utc == value.Kind)
                return value.ToLocalTime();

            return value;
        }

        /// <summary>
        /// Ensures that the DateTime value contains local time (converts if necessary).
        /// <locDE><para />Stellt sicher, dass der DateTime-Wert in Lokalzeit angegeben ist (konvertiert nötigenfalls).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kindIfUnspecified">The kind if unspecified (local or UTC).<locDE><para />Die Art, falls unspezifiziert (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value in local time.<locDE><para />DateTime-Wert in Lokalzeit.</locDE></returns>
        public static DateTime? EnsureLocal(this DateTime? value, DateTimeKind kindIfUnspecified)
        {
            if (!value.HasValue)
                return value;

            return EnsureLocal(value.Value, kindIfUnspecified);
        }
        #endregion
        #region EnsureUtc
        /// <summary>
        /// Ensures that the DateTime value contains UTC time (converts if necessary).
        /// <locDE><para />Stellt sicher, dass der DateTime-Wert in UTC angegeben ist (konvertiert nötigenfalls).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kindIfUnspecified">The kind if unspecified (local or UTC).<locDE><para />Die Art, falls unspezifiziert (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value in UTC.<locDE><para />DateTime-Wert in UTC.</locDE></returns>
        public static DateTime EnsureUtc(this DateTime value, DateTimeKind kindIfUnspecified)
        {
            value = value.KindIfUnspecified(kindIfUnspecified);

            if (DateTimeKind.Local == value.Kind)
                return value.ToUniversalTime();

            return value;
        }

        /// <summary>
        /// Ensures that the DateTime value contains UTC time (converts if necessary).
        /// <locDE><para />Stellt sicher, dass der DateTime-Wert in UTC angegeben ist (konvertiert nötigenfalls).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="kindIfUnspecified">The kind if unspecified (local or UTC).<locDE><para />Die Art, falls unspezifiziert (lokal oder UTC).</locDE></param>
        /// <returns>DateTime value in UTC.<locDE><para />DateTime-Wert in UTC.</locDE></returns>
        public static DateTime? EnsureUtc(this DateTime? value, DateTimeKind kindIfUnspecified)
        {
            if (!value.HasValue)
                return value;

            return EnsureUtc(value.Value, kindIfUnspecified);
        }
        #endregion

        #region TryParse
        /// <summary>
        /// Tries to parse a DateTime value from a string.
        /// Handles placeholder values like "NOW", etc.
        /// Returns true if successfully parsed.
        /// <locDE><para />Versucht, einen DateTime-Wert aus einem String zu parsen.
        /// Berücksichtigt Platzhalter-Werte wie "JETZT", etc.
        /// Liefert true, falls erfolgreich geparst.</locDE>
        /// </summary>
        /// <param name="source">The string to parse.<locDE><para />Der zu parsende String.</locDE></param>
        /// <param name="result">The resulting DateTime.<locDE><para />Der resultierende DateTime-Wert.</locDE></param>
        /// <param name="completeToToday">Should a time only value be completed to 'today'?
        /// <locDE><para />Soll eine Zeitangabe (ohne Datumsanteil) mit dem heutigen Datum ergänzt werden?</locDE></param>
        /// <returns>True if successfully parsed.<locDE><para />True, falls erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string source, out DateTime result, bool completeToToday)
        {
            return TryParse(source, out result, null, null, completeToToday);
        }

        /// <summary>
        /// Tries to parse a DateTime value from a string using german culture.
        /// Handles placeholder values like "NOW", etc.
        /// Returns true if successfully parsed.
        /// <locDE><para />Versucht, einen DateTime-Wert aus einem String zu parsen (mit deutscher Kultureinstellung).
        /// Berücksichtigt Platzhalter-Werte wie "JETZT", etc.
        /// Liefert true, falls erfolgreich geparst.</locDE>
        /// </summary>
        /// <param name="source">The string to parse.<locDE><para />Der zu parsende String.</locDE></param>
        /// <param name="result">The resulting DateTime.<locDE><para />Der resultierende DateTime-Wert.</locDE></param>
        /// <param name="formats">The exact formats to try (or empty = disabled).<locDE><para />Die genauen Formate, die versucht werden sollen (oder leer = deaktiviert).</locDE></param>
        /// <param name="keywordsNow">The keywords representing 'now' (or empty = disabled).<locDE><para />Die Schlüsselwörter/Platzhalter für 'jetzt' (oder leer = deaktiviert).</locDE></param>
        /// <param name="completeToToday">Should a time only value be completed to 'today'?
        /// <locDE><para />Soll eine Zeitangabe (ohne Datumsanteil) mit dem heutigen Datum ergänzt werden?</locDE></param>
        /// <returns>True if successfully parsed.<locDE><para />True, falls erfolgreich geparst.</locDE></returns>
        public static bool TryParseGerman(string source, out DateTime result, string[] formats = null,
            string[] keywordsNow = null, bool completeToToday = false)
        {
            return TryParse(source, out result, formats, keywordsNow, completeToToday, CultureHelper.GermanCulture);
        }

        /// <summary>
        /// Tries to parse a DateTime value from a string.
        /// Handles placeholder values like "NOW", etc.
        /// Returns true if successfully parsed.
        /// <locDE><para />Versucht, einen DateTime-Wert aus einem String zu parsen.
        /// Berücksichtigt Platzhalter-Werte wie "JETZT", etc.
        /// Liefert true, falls erfolgreich geparst.</locDE>
        /// </summary>
        /// <param name="source">The string to parse.<locDE><para />Der zu parsende String.</locDE></param>
        /// <param name="result">The resulting DateTime.<locDE><para />Der resultierende DateTime-Wert.</locDE></param>
        /// <param name="formats">The exact formats to try (or empty = disabled).<locDE><para />Die genauen Formate, die versucht werden sollen (oder leer = deaktiviert).</locDE></param>
        /// <param name="keywordsNow">The keywords representing 'now' (or empty = disabled).<locDE><para />Die Schlüsselwörter/Platzhalter für 'jetzt' (oder leer = deaktiviert).</locDE></param>
        /// <param name="completeToToday">Should a time only value be completed to 'today'?
        /// <locDE><para />Soll eine Zeitangabe (ohne Datumsanteil) mit dem heutigen Datum ergänzt werden?</locDE></param>
        /// <param name="ifp">The format provider (or null = current culture).<locDE><para />Der Format-Provider (oder null = aktuelle Kultur).</locDE></param>
        /// <returns>True if successfully parsed.<locDE><para />True, falls erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string source, out DateTime result, string[] formats,
            string[] keywordsNow = null, bool completeToToday = false, IFormatProvider ifp = null)
        {
            result = InvalidDateTime;

            if (null == source)
                return false;

            #region Handle 'now' keyword/placeholder values
            if (null == keywordsNow)
                keywordsNow = new string[] { "JETZT", "NOW" };

            if (keywordsNow.Length > 0)
            {
                // Keywords to be replaced were defined, check for them...
                string dtUppered = source.ToUpper();
                if (StringHelper.StartsWithOneOf(ref dtUppered, keywordsNow))
                {
                    result = DateTime.Now;
                    return true;
                }
            }
            #endregion

            System.Globalization.DateTimeStyles dateTimeStyles =
                //System.Globalization.DateTimeStyles.AssumeLocal |         // incompatible with RoundtripKind
                System.Globalization.DateTimeStyles.RoundtripKind |         // Preserve time zone information if contained (i.e. "Z(ulu)")
                System.Globalization.DateTimeStyles.NoCurrentDateDefault |
                System.Globalization.DateTimeStyles.AllowWhiteSpaces;

            #region Try generic parse using given/current culture
            if (null == ifp)
                ifp = System.Globalization.CultureInfo.CurrentCulture;

            if (DateTime.TryParse(source, ifp, dateTimeStyles, out result))
            {
                if (completeToToday)
                    result = CompleteToToday(result);
                return true;
            }
            #endregion

            #region Handle exact parsing (using format strings)
            // Detect partial entries, i.e. only hours and minutes
            // ISO8601: 2014-07-22T08:18:20+00:00,   2014-07-22T08:18:20Z,   2014-W30
            // 31.12.[20]14 23:59[:59]
            // [20]14-12-31 23:59[:59]
            // 12/31/[20]14 23:59[:59]
            // 31.12.[20]14
            // (X) 311214
            // 31.12.
            // 23:59:59
            // 235959
            // 23:59
            // 2359
            if (null == formats)
            {
                // No explicit format given, set default formats
                formats = new[]
                {
                    "dd.MM.yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "MM/dd/yyyy HH:mm:ss",
                    "dd.MM.yyyy HH:mm", "yyyy-MM-dd HH:mm", "MM/dd/yyyy HH:mm",
                    "dd.MM.yyyy", "yyyy-MM-dd", "MM/dd/yyyy",
                    "HH:mm:ss", "HH:mm", "HHmmss", "HHmm",
                    "dd.MM.", "MM-dd", "MM/dd",
                    "dd.MM.yy HH:mm:ss", "yy-MM-dd HH:mm:ss", "MM/dd/yy HH:mm:ss",
                    "dd.MM.yy HH:mm", "yy-MM-dd HH:mm", "MM/dd/yy HH:mm",
                    "dd.MM.yy", "yy-MM-dd", "MM/dd/yy"
                };
            }

            if (formats.Length > 0 &&
                DateTime.TryParseExact(source, formats, CultureHelper.InvariantCulture, dateTimeStyles, out result))
            {
                if (completeToToday)
                    result = CompleteToToday(result);
                return true;
            }
            #endregion

            return false;
        }

        /// <summary>
        /// Tries to parse a TimeSpan value from a string. 
        /// Handles formattings like "7d 12h 23m 34s" or "7 12:23:34" or "23 min 34 sec" or "34.56 sec". 
        /// Returns true if successfully parsed.
        /// <locDE><para />Versucht, eine TimeSpan-Wert aus einem String zu parsen.
        /// Berücksichtigt Formate wie "7d 12h 23m 34s" oder "7 12:23:34" oder "23 min 34 sec" oder "34.56 sec".
        /// Liefert true, falls erfolgreich geparst.</locDE>
        /// </summary>
        /// <param name="source">The string to parse.<locDE><para />Der zu parsende String.</locDE></param>
        /// <param name="result">The resulting TimeSpan.<locDE><para />Der resultierende TimeSpan-Wert.</locDE></param>
        /// <param name="smallestUnit">The smallest unit, i.e. 's'econds, 'm'inutes, 'h'ours, 'd'ays.
        /// <locDE><para />Die kleinste Einheit, z.B. 's'econds, 'm'inutes, 'h'ours, 'd'ays.</locDE></param>
        /// <returns>True if successfully parsed.<locDE><para />True, falls erfolgreich geparst.</locDE></returns>
        public static bool TryParse(string source, out TimeSpan result, char smallestUnit = 's')
        {
            // Examples:
            //       7 12:23:34
            //         12:23:34
            //         12:23        smallest unit parameter is needed to guess the correct unit
            //            23:34     smallest unit parameter is needed to guess the correct unit
            //               34     smallest unit parameter is needed to guess the correct unit
            // 7d 12h 23m    34s
            //    12h 23m    34s
            //    12h 23m
            //        23m    34s
            //    12h        34s
            //        23 m   34 s
            //        23 min 34 sec
            //                  45 msec
            // 7.5d
            //               34,45 sec

            result = TimeSpan.Zero;
            double days = 0;
            double hours = 0;
            double minutes = 0;
            double seconds = 0;
            double milliseconds = 0;
            char currentUnit = smallestUnit;

            // Count number of colons (2, 1 or 0)
            int countColons = source.CountOccurrence(':');
            if (2 == countColons)
                currentUnit = 's';

            // Replace all colons with space, "12:23:34" --> "12 23 34"
            source = source.ReplaceMulti(":| ");
            // Insert space between digit (or dot/comma) and letter, "23min" --> "23 min"
            source = source.InsertBetweenDigitAndLetter(" ");
            string[] items = source.Split(' ');
            if (null == items)
                return false;

            #region Parse split items
            foreach (string item in items.Reverse())
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;

                string cleanedItem = item.Trim().ToLowerInvariant();
                #region Try to recognize time unit, i.e. 's'econds, 'm'inutes, 'h'ours, 'd'ays
                if (cleanedItem.StartsWith("ms"))
                {
                    // msec
                    currentUnit = 'x';
                    continue;
                }
                if (cleanedItem.StartsWith("s"))
                {
                    // seconds
                    currentUnit = 's';
                    continue;
                }
                if (cleanedItem.StartsWith("m"))
                {
                    // minutes
                    currentUnit = 'm';
                    continue;
                }
                if (cleanedItem.StartsWith("h"))
                {
                    // hours
                    currentUnit = 'h';
                    continue;
                }
                if (cleanedItem.StartsWith("d") || cleanedItem.StartsWith("t"))
                {
                    // days
                    currentUnit = 'd';
                    continue;
                }
                #endregion

                if (!cleanedItem.ContainsOnlyDigits(".,+-"))
                    continue;

                double number = cleanedItem.ToDouble(0.0);
                switch (currentUnit)
                {
                    case 'x':
                        // msec
                        milliseconds += number;
                        currentUnit = 's';
                        break;
                    case 's':
                        // seconds
                        seconds += number;
                        currentUnit = 'm';
                        break;
                    case 'm':
                        // minutes
                        minutes += number;
                        currentUnit = 'h';
                        break;
                    case 'h':
                        // hours
                        hours += number;
                        currentUnit = 'd';
                        break;
                    case 'd':
                        // days
                        days += number;
                        currentUnit = ' ';
                        break;
                    default:
                        return false;
                }
            }
            #endregion

            #region Shift fractional parts to the next lower time unit
            if ((long)days != days)
            {
                double fraction = days - (long)days;
                hours += (24.0 * fraction);
            }
            if ((long)hours != hours)
            {
                double fraction = hours - (long)hours;
                minutes += (60.0 * fraction);
            }
            if ((long)minutes != minutes)
            {
                double fraction = minutes - (long)minutes;
                seconds += (60.0 * fraction);
            }
            if ((long)seconds != seconds)
            {
                double fraction = seconds - (long)seconds;
                milliseconds += (1000.0 * fraction);
            }
            #endregion

            #region Shift overflows to the next bigger time unit
            seconds += (long)(milliseconds / 1000.0);
            milliseconds = milliseconds % 1000.0;
            minutes += (long)(seconds / 60.0);
            seconds = seconds % 60.0;
            hours += (long)(minutes / 60.0);
            minutes = minutes % 60.0;
            days += (long)(hours / 24.0);
            hours = hours % 24.0;
            #endregion

            result = new TimeSpan((int)days, (int)hours, (int)minutes, (int)seconds, (int)milliseconds);
            return true;
        }
        #endregion

        #region ToStringDateAndOrTimeInvariant
        /// <summary>
        /// Formats the specified DateTime using invariant culture (yyyy-MM-dd HH:mm:ss), skips missing time or date portion.
        /// <locDE><para />Formatiert den angegebenen DateTime-Wert mit fixierter englischer Kultureinstellung (yyyy-MM-dd HH:mm:ss), lässt fehlenden Zeit-/Datumsanteil weg.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>DateTime value as formatted string.<locDE><para />DateTime-Wert als formatierter String.</locDE></returns>
        public static string ToStringDateAndOrTimeInvariant(this DateTime value)
        {
            if (null == value || InvalidDateTime == value)
                return string.Empty;

            if (IsTimeOnly(value))
                return value.ToString("HH:mm:ss");

            if (IsDateOnly(value))
                return value.ToString("yyyy-MM-dd");

            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion
        #region ToStringIso8601
        /// <summary>
        /// Formats the specified DateTime as defined in ISO8601, i.e. "2014-07-22T08:18:20+00:00".
        /// <locDE><para />Formatiert den angegebenen DateTime-Wert gemäß ISO8601, z.B. "2014-07-22T08:18:20+00:00".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>DateTime value as formatted string.<locDE><para />DateTime-Wert als formatierter String.</locDE></returns>
        public static string ToStringIso8601(this DateTime value)
        {
            if (null == value || InvalidDateTime == value)
                return string.Empty;

            // ISO8601: 
            // 2014-07-22T08:18:20+00:00
            // 2014-12-30T12:10:01.6304832Z
            // 2014-12-30T13:10:01.6304832+01:00
            return value.ToString("o");
        }
        #endregion
        #region ToStringAuto
        /// <summary>
        /// Formats a TimeSpan as "7d 12h 23m 34s" or "7 12:23:34".
        /// <locDE><para />Formatiert den angegebenen TimeSpan-Wert als "7d 12h 23m 34s" oder "7 12:23:34".</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="smallestUnit">The smallest unit, i.e. 's'econds, 'm'inutes, 'h'ours, 'd'ays.
        /// <locDE><para />Die kleinste Einheit, z.B. 's'econds, 'm'inutes, 'h'ours, 'd'ays.</locDE></param>
        /// <param name="letterDelimiters">Should letters d/h/m/s be inserted as delimiters (true) or colon characters (false)?
        /// <locDE><para />Sollen die Buchstaben d/h/m/s als Trennzeichen eingefügt werden (true) oder Doppelpunkte (false)?</locDE></param>
        /// <param name="skipNullValuesInside">Should all inside null values be skipped, i.e. "1d 00h 02m" becomes "1d 02m"? Only valid with letter delimiters!
        /// <locDE><para />Sollen eingeschlossene Null-Werte weggelassen werden, z.B. "1d 00h 02m" wird zu "1d 02m"? Nur in Verbindung mit Buchstaben-Trennzeichen!</locDE></param>
        /// <returns>TimeSpan value as formatted string.<locDE><para />TimeSpan-Wert als formatierter String.</locDE></returns>
        public static string ToStringAuto(this TimeSpan value, char smallestUnit = 'm', bool letterDelimiters = true, bool skipNullValuesInside = true)
        {
            // NOTE: TimeSpan may be negative!

            // Round seconds up, if necessary (we don't show milliseconds)
            //ts = ts.Add(TimeSpan.FromMilliseconds(500));

            string format = "";
            switch (smallestUnit)
            {
                case 'd':
                case 'D':
                    if (value.Days != 0)
                    {
                        if (letterDelimiters)
                            format += "d'd'";
                        else
                            format += "d";
                    }
                    break;
                case 'h':
                case 'H':
                    if (value.Days != 0)
                    {
                        if (letterDelimiters)
                            format += "d'd '";
                        else
                            format += "d' '";
                    }
                    if (value.Hours != 0 || !skipNullValuesInside || 0 == format.Length)
                    {
                        if (letterDelimiters)
                            format += "hh'h'";
                        else
                            format += "hh";
                    }
                    break;
                case 'm':
                case 'M':
                    if (value.Days != 0)
                    {
                        if (letterDelimiters)
                            format += "d'd '";
                        else
                            format += "d' '";
                    }
                    if (value.Hours != 0 || (!skipNullValuesInside && 0 != format.Length))
                    {
                        if (letterDelimiters)
                            format += "hh'h '";
                        else
                            format += "hh':'";
                    }
                    if (value.Minutes != 0 || !skipNullValuesInside || 0 == format.Length)
                    {
                        if (letterDelimiters)
                            format += "mm'm'";
                        else
                            format += "mm";
                    }
                    break;
                //case 's':
                //case 'S':
                default:
                    if (value.Days != 0)
                    {
                        if (letterDelimiters)
                            format += "d'd '";
                        else
                            format += "d' '";
                    }
                    if (value.Hours != 0 || (!skipNullValuesInside && 0 != format.Length))
                    {
                        if (letterDelimiters)
                            format += "hh'h '";
                        else
                            format += "hh':'";
                    }
                    if (value.Minutes != 0 || (!skipNullValuesInside && 0 != format.Length))
                    {
                        if (letterDelimiters)
                            format += "mm'm '";
                        else
                            format += "mm':'";
                    }
                    if (value.Seconds != 0 || !skipNullValuesInside || 0 == format.Length)
                    {
                        if (letterDelimiters)
                            format += "ss's'";
                        else
                            format += "ss";
                    }
                    break;
            }

            // Cut leftmost two-digit parameter to avoid leading zero: mm --> m, 00..59 --> 0..59
            if (format.StartsWith("hh") || format.StartsWith("mm") || format.StartsWith("ss"))
                format = format.Substring(1);

            string result = value.ToString(format).TrimEnd(new char[] { ' ', ':' });
            // If TimeSpan is negative, add minus sign
            if (value.TotalSeconds < 0)
                result = "-" + result;
            return result;
        }
        #endregion

        #region CompleteToToday
        /// <summary>
		/// Completes to today if no/invalid date is set (only time portion valid).
        /// <locDE><para />Vervollständigt einen DateTime-Wert, der nur einen gültigen Zeitanteil hat, mit dem heutigen Datum.</locDE>
		/// </summary>
		/// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
		/// <returns>DateTime value with valid date portion set.<locDE><para />DateTime-Wert mit gültigem Datumsanteil.</locDE></returns>
		public static DateTime CompleteToToday(this DateTime value)
        {
            return CompleteToDay(value, DateTime.Today);
        }

        /// <summary>
		/// Completes to today if no/invalid date is set (only time portion valid).
        /// <locDE><para />Vervollständigt einen DateTime-Wert, der nur einen gültigen Zeitanteil hat, mit dem heutigen Datum.</locDE>
		/// </summary>
		/// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="day">The date portion to set (if necessary).<locDE><para />Der zu setzende Datumsanteil (falls nötig).</locDE></param>
		/// <returns>DateTime value with valid date portion set.<locDE><para />DateTime-Wert mit gültigem Datumsanteil.</locDE></returns>
        public static DateTime CompleteToDay(this DateTime value, DateTime day)
        {
            DateTime dtCompleted = value;
            if (value.Year <= 1)
            {
                dtCompleted = dtCompleted.AddDays(day.Day - dtCompleted.Day);
                dtCompleted = dtCompleted.AddMonths(day.Month - dtCompleted.Month);
                dtCompleted = dtCompleted.AddYears(day.Year - dtCompleted.Year);
            }
            return dtCompleted;
        }
        #endregion

        #region RoundUp
        /// <summary>
        /// Rounds the given DateTime value up to an interval, i.e. 30 mins: 07:55 gets 08:00 or 08:01 gets 08:30.
        /// <locDE><para />Rundet den angegebenen DateTime-Wert zu einem Intervall auf, z.B. 30 Minuten: 07:55 wird zu 08:00 oder 08:01 wird zu 08:30.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="interval">The interval to round up, i.e. TimeSpan.FromMinutes(30).
        /// <locDE><para />Der Intervall, zu dem aufgerundet werden soll, z.B. TimeSpan.FromMinutes(30).</locDE></param>
        /// <returns>DateTime value rounded up to the next interval.<locDE><para />DateTime-Wert, der zum nächsten Intervall aufgerundet wurde.</locDE></returns>
        public static DateTime RoundUp(this DateTime value, TimeSpan interval)
        {
            // https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes

            DateTime dtMinusOneSec = value.Subtract(TimeSpan.FromSeconds(1));
            return new DateTime(((dtMinusOneSec.Ticks + interval.Ticks - 1) / interval.Ticks) * interval.Ticks, value.Kind);
            //var modTicks = value.Ticks % interval.Ticks;
            //var delta = modTicks != 0 ? interval.Ticks - modTicks : 0;
            //return new DateTime(value.Ticks + delta, value.Kind);
        }
        #endregion
        #region RoundDown
        /// <summary>
        /// Rounds the given DateTime value down to an interval, i.e. 30 mins: 07:55 gets 07:30 or 08:01 gets 08:30.
        /// <locDE><para />Rundet den angegebenen DateTime-Wert zu einem Intervall ab, z.B. 30 Minuten: 07:55 wird zu 07:30 oder 08:01 wird zu 08:30.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="interval">The interval to round down, i.e. TimeSpan.FromMinutes(30).
        /// <locDE><para />Der Intervall, zu dem abgerundet werden soll, z.B. TimeSpan.FromMinutes(30).</locDE></param>
        /// <returns>DateTime value rounded down to the next interval.<locDE><para />DateTime-Wert, der zum nächsten Intervall abgerundet wurde.</locDE></returns>
        public static DateTime RoundDown(this DateTime value, TimeSpan interval)
        {
            // https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes

            var delta = value.Ticks % interval.Ticks;
            return new DateTime(value.Ticks - delta, value.Kind);
        }
        #endregion
        #region Round
        /// <summary>
        /// Rounds the given DateTime value to an interval, i.e. 60 mins: 07:55 gets 08:00 or 08:25 gets 08:00.
        /// <locDE><para />Rundet den angegebenen DateTime-Wert zu einem Intervall, z.B. 60 Minuten: 07:55 wird zu 08:00 oder 08:25 wird zu 08:00.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="interval">The interval to round, i.e. TimeSpan.FromMinutes(60).
        /// <locDE><para />Der Intervall, zu dem gerundet werden soll, z.B. TimeSpan.FromMinutes(60).</locDE></param>
        /// <returns>DateTime value rounded to the next interval.<locDE><para />DateTime-Wert, der zum nächsten Intervall gerundet wurde.</locDE></returns>
        public static DateTime Round(this DateTime value, TimeSpan interval)
        {
            // https://stackoverflow.com/questions/7029353/how-can-i-round-up-the-time-to-the-nearest-x-minutes

            var delta = value.Ticks % interval.Ticks;
            bool roundUp = delta > interval.Ticks / 2;
            var offset = roundUp ? interval.Ticks : 0;
            return new DateTime(value.Ticks + offset - delta, value.Kind);
        }
        #endregion

        #region GetAgeInYears
        /// <summary>
        /// Calculates the age (in years).
        /// <locDE><para />Berechnet das Alter (in Jahren).</locDE>
        /// </summary>
        /// <param name="birthDate">The birth date.<locDE><para />Das Geburtsdatum.</locDE></param>
        /// <returns>Age in years.<locDE><para />Alter in Jahren.</locDE></returns>
        public static int GetAgeInYears(this DateTime birthDate)
        {
            // How do I calculate someone's age in C#?
            // http://stackoverflow.com/a/1404

            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Calculates the age (in years).
        /// <locDE><para />Berechnet das Alter (in Jahren).</locDE>
        /// </summary>
        /// <param name="birthYear">The birth year.<locDE><para />Das Geburtsjahr.</locDE></param>
        /// <returns>Age in years.<locDE><para />Alter in Jahren.</locDE></returns>
        /// <returns></returns>
        public static int GetAgeInYears(int birthYear)
        {
            return GetAgeInYears(new DateTime(birthYear, 1, 1));
        }
        #endregion

        #region GetWeekOfYear
        /// <summary>
        /// Gets the calendar week number (1..53).
        /// <locDE><para />Ermittelt die Kalenderwoche (1..53).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Calendar week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></returns>
        public static int GetWeekOfYear(this DateTime value)
        {
            return GetWeekOfYear(value, CultureHelper.InvariantCulture, DefaultCalendarWeekRule, DefaultFirstDayOfWeek);
        }

        /// <summary>
        /// Gets the calendar week number (1..53).
        /// <locDE><para />Ermittelt die Kalenderwoche (1..53).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <returns>Calendar week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></returns>
        public static int GetWeekOfYear(this DateTime value, System.Globalization.CultureInfo ci)
        {
            return GetWeekOfYear(value, ci, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Gets the calendar week number (1..53).
        /// <locDE><para />Ermittelt die Kalenderwoche (1..53).</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="calWeekRule">The calendar week rule (typ. FirstFourDayWeek).</param>
        /// <param name="firstDayOfWeek">The first day of week (typ. Monday).<locDE><para />Der erste Tag der Woche (typ. Montag).</locDE></param>
        /// <returns>Calendar week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></returns>
        public static int GetWeekOfYear(this DateTime value, System.Globalization.CultureInfo ci,
            System.Globalization.CalendarWeekRule calWeekRule, System.DayOfWeek firstDayOfWeek)
        {
            return ci.Calendar.GetWeekOfYear(value, calWeekRule, firstDayOfWeek);
        }
        #endregion
        #region GetWeekOfYearIso8601
        /// <summary>
        /// Gets the calendar week according to ISO8601 (1..54).
        /// <locDE><para />Ermittelt die Kalenderwoche gemäß ISO8601 (1..54).</locDE>
        /// </summary>
        /// <remarks>
        /// See http://stackoverflow.com/a/11155102/284240
        /// <locDE><para />Siehe http://stackoverflow.com/a/11155102/284240 </locDE>
        /// </remarks>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Calendar week number according to ISO8601 (1..54).<locDE><para />Die Kalenderwoche gemäß ISO8601 (1..54).</locDE></returns>
        public static int GetWeekOfYearIso8601(this DateTime value)
        {
            return GetWeekOfYearIso8601(value, CultureHelper.InvariantCulture);
        }

        /// <summary>
        /// Gets the calendar week according to ISO8601 (1..54).
        /// <locDE><para />Ermittelt die Kalenderwoche gemäß ISO8601 (1..54).</locDE>
        /// </summary>
        /// <remarks>
        /// See http://stackoverflow.com/a/11155102/284240
        /// <locDE><para />Siehe http://stackoverflow.com/a/11155102/284240 </locDE>
        /// </remarks>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <returns>Calendar week number according to ISO8601 (1..54).<locDE><para />Die Kalenderwoche gemäß ISO8601 (1..54).</locDE></returns>
        public static int GetWeekOfYearIso8601(this DateTime value, System.Globalization.CultureInfo ci)
        {
            return GetWeekOfYearIso8601(value, ci, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Gets the calendar week according to ISO8601 (1..54).
        /// <locDE><para />Ermittelt die Kalenderwoche gemäß ISO8601 (1..54).</locDE>
        /// </summary>
        /// <remarks>
        /// See http://stackoverflow.com/a/11155102/284240
        /// <locDE><para />Siehe http://stackoverflow.com/a/11155102/284240 </locDE>
        /// </remarks>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="calWeekRule">The calendar week rule (typ. FirstFourDayWeek).</param>
        /// <param name="firstDayOfWeek">The first day of week (typ. Monday).<locDE><para />Der erste Tag der Woche (typ. Montag).</locDE></param>
        /// <returns>Calendar week number according to ISO8601 (1..54).<locDE><para />Die Kalenderwoche gemäß ISO8601 (1..54).</locDE></returns>
        public static int GetWeekOfYearIso8601(this DateTime value, System.Globalization.CultureInfo ci,
            System.Globalization.CalendarWeekRule calWeekRule, System.DayOfWeek firstDayOfWeek)
        {
            DayOfWeek day = ci.Calendar.GetDayOfWeek(value);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                value = value.AddDays(3);
            }

            return ci.Calendar.GetWeekOfYear(value, calWeekRule, firstDayOfWeek);
        }
        #endregion

        #region GetFirstDateOfWeek
        /// <summary>
        /// Gets the first date of a given week.
        /// <locDE><para />Ermittelt das Datum des ersten Tages der angegebenen Kalenderwoche.</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <returns>Date of the first day of given week.<locDE><para />Datum des ersten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetFirstDateOfWeek(int weekOfYear, int year = 0)
        {
            return GetFirstDateOfWeek(weekOfYear, CultureHelper.InvariantCulture, year, DefaultCalendarWeekRule, DefaultFirstDayOfWeek);
        }

        /// <summary>
        /// Gets the first date of a given week.
        /// <locDE><para />Ermittelt das Datum des ersten Tages der angegebenen Kalenderwoche.</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <returns>Date of the first day of given week.<locDE><para />Datum des ersten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetFirstDateOfWeek(int weekOfYear, System.Globalization.CultureInfo ci, int year = 0)
        {
            return GetFirstDateOfWeek(weekOfYear, ci, year, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Gets the first date of a given week.
        /// <locDE><para />Ermittelt das Datum des ersten Tages der angegebenen Kalenderwoche.</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <param name="calWeekRule">The calendar week rule (typ. FirstFourDayWeek).</param>
        /// <param name="firstDayOfWeek">The first day of week (typ. Monday).<locDE><para />Der erste Tag der Woche (typ. Montag).</locDE></param>
        /// <returns>Date of the first day of given week.<locDE><para />Datum des ersten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetFirstDateOfWeek(int weekOfYear, System.Globalization.CultureInfo ci, int year,
            System.Globalization.CalendarWeekRule calWeekRule, System.DayOfWeek firstDayOfWeek)
        {
            if (0 == year)
                year = DateTime.Now.Year;

            // http://stackoverflow.com/questions/19901666/get-date-of-first-and-last-day-of-week-knowing-week-number
            #region Old code
            //DateTime jan1 = new DateTime(year, 1, 1);
            ////int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            //int daysOffset = (int)firstDayOfWeek - (int)jan1.DayOfWeek;
            //DateTime firstWeekDay = jan1.AddDays(daysOffset);
            ////int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            //int firstWeek = GetWeekOfYear(jan1, ci, calWeekRule, firstDayOfWeek);
            //if (firstWeek <= 1 || firstWeek > 50)
            //{
            //    weekOfYear -= 1;
            //}
            //return firstWeekDay.AddDays(weekOfYear * 7);
            #endregion

            var newYear = new DateTime(year, 1, 1);
            var weekNumber = newYear.GetWeekOfYearIso8601(ci, calWeekRule, firstDayOfWeek);

            DateTime firstWeekDate;

            if (weekNumber != 1)
            {
                var dayNumber = (int)newYear.DayOfWeek;
                firstWeekDate = newYear.AddDays(7 - dayNumber + 1);
            }
            else
            {
                var dayNumber = (int)newYear.DayOfWeek;
                firstWeekDate = newYear.AddDays(-dayNumber + 1);
            }

            if (weekOfYear == 1)
            {
                return firstWeekDate;
            }
            return firstWeekDate.AddDays(7 * (weekOfYear - 1));
        }
        #endregion
        #region GetLastDateOfWeek
        /// <summary>
        /// Gets the last date of a given week (first date + 6 days).
        /// <locDE><para />Ermittelt das Datum des letzten Tages der angegebenen Kalenderwoche (erster Tag + 6 Tage).</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <returns>Date of the last day of given week.<locDE><para />Datum des letzten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetLastDateOfWeek(int weekOfYear, int year = 0)
        {
            return GetLastDateOfWeek(weekOfYear, CultureHelper.InvariantCulture, year, DefaultCalendarWeekRule, DefaultFirstDayOfWeek);
        }

        /// <summary>
        /// Gets the last date of a given week (first date + 6 days).
        /// <locDE><para />Ermittelt das Datum des letzten Tages der angegebenen Kalenderwoche (erster Tag + 6 Tage).</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <returns>Date of the last day of given week.<locDE><para />Datum des letzten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetLastDateOfWeek(int weekOfYear, System.Globalization.CultureInfo ci, int year = 0)
        {
            return GetLastDateOfWeek(weekOfYear, ci, year, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Gets the last date of a given week (first date + 6 days).
        /// <locDE><para />Ermittelt das Datum des letzten Tages der angegebenen Kalenderwoche (erster Tag + 6 Tage).</locDE>
        /// </summary>
        /// <param name="weekOfYear">The week number (1..53).<locDE><para />Die Kalenderwoche (1..53).</locDE></param>
        /// <param name="ci">The culture info to use.<locDE><para />Das zu verwendende CultureInfo Objekt.</locDE></param>
        /// <param name="year">The year (0 = current year).<locDE><para />Das Jahr (0 = aktuelles Jahr).</locDE></param>
        /// <param name="calWeekRule">The calendar week rule (typ. FirstFourDayWeek).</param>
        /// <param name="firstDayOfWeek">The first day of week (typ. Monday).<locDE><para />Der erste Tag der Woche (typ. Montag).</locDE></param>
        /// <returns>Date of the last day of given week.<locDE><para />Datum des letzten Tages der angegebenen Woche.</locDE></returns>
        public static DateTime GetLastDateOfWeek(int weekOfYear, System.Globalization.CultureInfo ci, int year,
            System.Globalization.CalendarWeekRule calWeekRule, System.DayOfWeek firstDayOfWeek)
        {
            return GetFirstDateOfWeek(weekOfYear, ci, year, calWeekRule, firstDayOfWeek).AddDays(6);
        }
        #endregion

        #region Catholic holidays
        /// <summary>
        /// Catholic holidays.
        /// </summary>
        public static class CatholicHoliday
        {
            // https://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
            // http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year

            #region Easter sunday
            /// <summary>
            /// Calculates easter sunday for the given year.
            /// </summary>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime EasterSunday(int year)
            {
                int day = 0;
                int month = 0;

                int g = year % 19;
                int c = year / 100;
                int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
                int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

                day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
                month = 3;

                if (day > 31)
                {
                    month++;
                    day -= 31;
                }

                return new DateTime(year, month, day);
            }
            #endregion

            #region GoodFriday
            /// <summary>
            /// Calculates Good Friday for the given year.
            /// </summary>
            /// <remarks>
            /// Good Friday is the Friday before easter.
            /// </remarks>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime GoodFriday(int year)
            {
                return EasterSunday(year).AddDays(-2);
            }
            #endregion

            #region PalmSunday
            /// <summary>
            /// Calculates Palm Sunday for the given year.
            /// </summary>
            /// <remarks>
            /// Palm Sunday is the sunday one week before easter.
            /// </remarks>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime PalmSunday(int year)
            {
                return EasterSunday(year).AddDays(-7);
            }
            #endregion

            #region AscensionDay
            /// <summary>
            /// Calculates Ascencion day for the given year.
            /// </summary>
            /// <remarks>Ascencion day is always 10 days before Whit Sunday.</remarks>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime AscensionDay(int year)
            {
                return EasterSunday(year).AddDays(39);
            }
            #endregion

            #region WhitSunday
            /// <summary>
            /// Calculates Whit Sunday for the given year.
            /// </summary>
            /// <remarks>Whit Sunday is always 7 weeks after Easter.</remarks>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime WhitSunday(int year)
            {
                return EasterSunday(year).AddDays(49);
            }
            #endregion

            #region AshWednesday
            /// <summary>
            /// Calculates Ash Wednesday for the given year.
            /// </summary>
            /// <remarks>
            /// Ash Wednesday marks the start of Lent. This is the 40 day period between before Easter.
            /// </remarks>
            /// <param name="year">The year (not before 1583).</param>
            /// <returns></returns>
            public static DateTime AshWednesday(int year)
            {
                return EasterSunday(year).AddDays(-46);
            }
            #endregion

            #region FirstSundayOfAdvent
            /// <summary>
            /// Calculates the first sunday of advent for the given year.
            /// </summary>
            /// <remarks>
            /// The first sunday of advent is the first sunday at least 4 weeks before christmas.
            /// </remarks>
            /// <param name="year">The year.</param>
            /// <returns></returns>
            public static DateTime FirstSundayOfAdvent(int year)
            {
                int weeks = 4;
                int correction = 0;
                DateTime christmas = new DateTime(year, 12, 25);

                if (christmas.DayOfWeek != DayOfWeek.Sunday)
                {
                    weeks--;
                    correction = ((int)christmas.DayOfWeek - (int)DayOfWeek.Sunday);
                }
                return christmas.AddDays(-1 * ((weeks * 7) + correction));
            }
            #endregion

            #region ChristmasDay
            /// <summary>
            /// Calculates the first day of christmas for the given year.
            /// </summary>
            /// <remarks>
            /// Is always on December 25.
            /// </remarks>
            /// <param name="year">The year.</param>
            /// <returns></returns>
            public static DateTime ChristmasDay(int year)
            {
                return new DateTime(year, 12, 25);
            }
            #endregion
        }
        #endregion
    }
}
