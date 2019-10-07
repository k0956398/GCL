namespace EplusE
{
    /// <summary>
    /// Int64 extension methods.
    /// <locDE><para />Int64 Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class Int64Extension
    {
        #region ValueOrNullIfZero

        /// <summary>
        /// Gets the long value or null if value is 0.
        /// <locDE><para />Holt den Long-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or long value.<locDE><para />Null oder Long-Wert.</locDE></returns>
        public static long? ValueOrNullIfZero(this long value)
        {
            if (0 == value)
                return null;

            return value;
        }

        /// <summary>
        /// Gets the long value or null if value is 0.
        /// <locDE><para />Holt den Long-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or long value.<locDE><para />Null oder Long-Wert.</locDE></returns>
        public static long? ValueOrNullIfZero(this long? value)
        {
            if (null == value || 0 == (long)value)
                return null;

            return value;
        }

        #endregion ValueOrNullIfZero

        #region ValueOrZeroIfNull

        /// <summary>
        /// Gets the long value or 0 if null.
        /// <locDE><para />Holt den Long-Wert oder 0 bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Long value or 0 (if null).<locDE><para />Long-Wert oder 0 (falls Null).</locDE></returns>
        public static long ValueOrZeroIfNull(this long? value)
        {
            if (null == value || 0 == (long)value)
                return 0;

            return (long)value;
        }

        #endregion ValueOrZeroIfNull
    }
}