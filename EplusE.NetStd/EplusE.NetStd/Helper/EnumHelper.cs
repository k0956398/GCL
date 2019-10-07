using System;
using System.Collections.Generic;

namespace EplusE
{
    /// <summary>
    /// EnumHelper class, i.e. enum value to name/translation for display.
    /// <locDE><para />EnumHelper Klasse, z.B. Enum-Wert zu Klartext-Name/Übersetzung zur Anzeige.</locDE>
    /// </summary>
    public static class EnumHelper
    {
        #region RegisteredEnums

        /// <summary>
        /// The registered enum types and their names/translations for display .
        /// <locDE><para />Die registrierten Enum-Typen und deren Klartext-Namen/Übersetzungen zur Anzeige.</locDE>
        /// </summary>
        private static IDictionary<Type, IDictionary<object, string>> RegisteredEnums;

        #endregion RegisteredEnums

        #region Static constructor

        /// <summary>
        /// Static constructor of the <see cref="EnumHelper"/> class.
        /// <locDE><para />Statischer Konstruktor der <see cref="EnumHelper"/> Klasse.</locDE>
        /// </summary>
        static EnumHelper()
        {
            RegisteredEnums = new Dictionary<Type, IDictionary<object, string>>();
        }

        #endregion Static constructor

        #region RegisterEnum

        /// <summary>
        /// Registers a enum type and its names/translations for display.
        /// <locDE><para />Registriert einen Enum-Typen und dessen Klartext-Namen/Übersetzungen zur Anzeige.</locDE>
        /// </summary>
        /// <param name="type">The enum type.<locDE><para />Der Enum-Typ.</locDE></param>
        /// <param name="values">The value pairs of enum value and name/translation for Display.
        /// <locDE><para />Die Wertepaare von Enum-Wert und Klartext-Name/Übersetzung zur Anzeige.</locDE></param>
        public static void RegisterEnum(Type type, IDictionary<object, string> values)
        {
            RegisteredEnums[type] = values;
        }

        #endregion RegisterEnum

        #region GetEnumValuesOf

        /// <summary>
        /// Gets the enum values of the given type.
        /// <locDE><para />Holt die Enum-Werte des angegebenen Typs.</locDE>
        /// </summary>
        /// <param name="type">The enum type.<locDE><para />Der Enum-Typ.</locDE></param>
        /// <returns>The enum values of the given type.<locDE><para />Die Enum-Werte des angegebenen Typs.</locDE></returns>
        public static IDictionary<object, string> GetEnumValuesOf(Type type)
        {
            IDictionary<object, string> dict;
            if (!RegisteredEnums.TryGetValue(type, out dict))
                return null;

            return dict;
        }

        #endregion GetEnumValuesOf

        #region GetText

        /// <summary>
        /// Gets the name/translation for display text.
        /// <locDE><para />Holt den Klartext-Namen/Übersetzungs-Text.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <param name="nullIfNoValue">Return null if no value found?<locDE><para />Null zurückgeben, falls kein Wert gefunden?</locDE></param>
        /// <returns>The name/translation for display text.<locDE><para />Der Klartext-Name/Übersetzungs-Text.</locDE></returns>
        public static string GetText(object value, bool nullIfNoValue = true)
        {
            if (null == value)
            {
                if (nullIfNoValue)
                    return null;
                return "";
            }

            IDictionary<object, string> dict;
            if (!RegisteredEnums.TryGetValue(value.GetType(), out dict))
            {
                if (nullIfNoValue)
                    return null;
                return value.ToStringInvariant();
            }

            string text;
            if (!dict.TryGetValue(value, out text))
            {
                if (nullIfNoValue)
                    return null;
                return value.ToStringInvariant();
            }

            return text ?? "";
        }

        #endregion GetText

        #region ToLocalizedText

        /// <summary>
        /// Gets the name/translation for display text.
        /// <locDE><para />Holt den Klartext-Namen/Übersetzungs-Text.</locDE>
        /// </summary>
        /// <param name="value">The value.<locDE><para />Der Wert.</locDE></param>
        /// <returns>The name/translation for display text.<locDE><para />Der Klartext-Name/Übersetzungs-Text.</locDE></returns>
        public static string ToLocalizedText(this Enum value)
        {
            return GetText(value, false);
        }

        #endregion ToLocalizedText

        #region TryParse

        /// <summary>
        /// Tries to parse the translated display text back to an enum value.
        /// <locDE><para />Versucht, den ursprünglichen Enum-Wert anhand des übersetzten Klartext-Namens zu rekonstruieren.</locDE>
        /// </summary>
        /// <param name="type">The enum type.<locDE><para />Der Enum-Typ.</locDE></param>
        /// <param name="value">The value (translated display text).<locDE><para />Der Wert (übersetzter Klartext-Name).</locDE></param>
        /// <returns>The enum value of the given type or null.<locDE><para />Der passende Enum-Wert des angegebenen Typs order Null.</locDE></returns>
        public static object TryParse(Type type, object value)
        {
            if (null == value)
                return null;

            IDictionary<object, string> dict;
            if (!RegisteredEnums.TryGetValue(type, out dict))
                return null;

            foreach (KeyValuePair<object, string> kvp in dict)
            {
                if (kvp.Value.EqualsSafeIgnoreCase(value))
                    return kvp.Key;
            }

            return null;
        }

        #endregion TryParse
    }
}