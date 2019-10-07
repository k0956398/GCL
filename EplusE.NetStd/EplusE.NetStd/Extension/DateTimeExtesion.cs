using System;

namespace EplusE
{
    /// <summary>
    /// DateTime extension methods.
    /// </summary>
    public static class DateTimeExtesion
    {
        /// <summary>
        /// Invalid DateTime value: 0001-01-01 00:00:00 UTC.
        /// </summary>
        public static readonly DateTime InvalidDateTime = new DateTime(1, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Determines whether two DateTime values have the same date (= day). Time portion is ignored.
        /// </summary>
        /// <param name="date1">The date #1.</param>
        /// <param name="date2">The date #2.</param>
        /// <returns>True if DateTime values are of the same day.</returns>
        public static bool IsSameDay(this DateTime date1, DateTime date2)
        {
            return (null != date1 && null != date2 && date1.Year == date2.Year && date1.DayOfYear == date2.DayOfYear);
        }

        /// <summary>
        /// Determines whether DateTime value contains only a time (all date components are 0).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if DateTime value is just a time.</returns>
        public static bool IsTimeOnly(this DateTime value)
        {
            return (null != value && value.Year <= 1 && value.Month <= 1 && value.Day <= 1);
        }

        /// <summary>
        /// Determines whether the DateTime value is of today. Time portion is ignored.
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>True if DateTime value is of today.</returns>
        public static bool IsToday(this DateTime value)
        {
            return IsSameDay(value, DateTime.Today);
        }

        /// <summary>
        /// Formats the specified DateTime as defined in ISO8601, i.e. "2014-07-22T08:18:20+00:00".
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>DateTime value as formatted string.</returns>
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
    }
}