namespace EplusE
{
    /// <summary>
    /// Int32 extension methods.
    /// <locDE><para />Int32 Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class Int32Extension
    {
        #region ValueOrNullIfZero

        /// <summary>
        /// Gets the int value or null if value is 0.
        /// <locDE><para />Holt den Int-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or int value.<locDE><para />Null oder Int-Wert.</locDE></returns>
        public static int? ValueOrNullIfZero(this int value)
        {
            if (0 == value)
                return null;

            return value;
        }

        /// <summary>
        /// Gets the int value or null if value is 0.
        /// <locDE><para />Holt den Int-Wert oder Null falls Wert = 0 ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or int value.<locDE><para />Null oder Int-Wert.</locDE></returns>
        public static int? ValueOrNullIfZero(this int? value)
        {
            if (null == value || 0 == (int)value)
                return null;

            return value;
        }

        #endregion ValueOrNullIfZero

        #region ValueOrZeroIfNull

        /// <summary>
        /// Gets the int value or 0 if null.
        /// <locDE><para />Holt den Int-Wert oder 0 bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Int value or 0 (if null).<locDE><para />Int-Wert oder 0 (falls Null).</locDE></returns>
        public static int ValueOrZeroIfNull(this int? value)
        {
            if (null == value || 0 == (int)value)
                return 0;

            return (int)value;
        }

        #endregion ValueOrZeroIfNull
    }
}