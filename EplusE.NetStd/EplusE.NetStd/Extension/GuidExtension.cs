using System;

namespace EplusE
{
    /// <summary>
    /// Guid extension methods.
    /// <locDE><para />Guid Erweiterungsmethoden.</locDE>
    /// </summary>
    public static class GuidExtension
    {
        #region ValueOrNullIfEmpty

        /// <summary>
        /// Gets the Guid value or null if Guid is empty.
        /// <locDE><para />Holt den Guid-Wert oder Null falls Guid leer ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or Guid value.<locDE><para />Null oder Guid-Wert.</locDE></returns>
        public static Guid? ValueOrNullIfEmpty(this Guid value)
        {
            if (Guid.Empty == value)
                return null;

            return value;
        }

        /// <summary>
        /// Gets the Guid value or null if Guid is empty.
        /// <locDE><para />Holt den Guid-Wert oder Null falls Guid leer ist.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Null or Guid value.<locDE><para />Null oder Guid-Wert.</locDE></returns>
        public static Guid? ValueOrNullIfEmpty(this Guid? value)
        {
            if (null == value || Guid.Empty == (Guid)value)
                return null;

            return value;
        }

        #endregion ValueOrNullIfEmpty

        #region ValueOrEmptyIfNull

        /// <summary>
        /// Gets the Guid value or Guid.Empty if null.
        /// <locDE><para />Holt den Guid-Wert oder Guid.Empty bei Null.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>Guid value or Guid.Empty (if null).<locDE><para />Guid-Wert oder Guid.Empty (falls Null).</locDE></returns>
        public static Guid ValueOrEmptyIfNull(this Guid? value)
        {
            if (null == value || Guid.Empty == (Guid)value)
                return Guid.Empty;

            return (Guid)value;
        }

        #endregion ValueOrEmptyIfNull
    }
}