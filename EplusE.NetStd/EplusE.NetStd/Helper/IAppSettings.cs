using System;

namespace EplusE
{
    /// <summary>
    /// Application settings access interface.
    /// <locDE><para />Anwendungskonfiguration Zugriffs-Interface.</locDE>
    /// </summary>
    public interface IAppSettings
    {
        #region GetValue
        /// <summary>
		/// Gets the value for the specified key from the persistent storage (app.config, etc).
        /// <locDE><para />Holt den Wert für den angegebenen Schlüsselbegriff aus dem Konfigurationsspeicher (app.config, etc).</locDE>
		/// </summary>
		/// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
		/// <param name="defaultValue">The default value.<locDE><para />Der Vorgabewert.</locDE></param>
		/// <returns>Loaded value or default value if not available.<locDE><para />Geladener Wert oder Vorgabewert falls nicht vorhanden.</locDE></returns>
		string GetValue(string key, string defaultValue = "");
        #endregion

        #region SetValue
        /// <summary>
        /// Sets the value for the specified key in the persistent storage (app.config, etc).
        /// <locDE><para />Schreibt den Wert für den angegebenen Schlüsselbegriff in den Konfigurationsspeicher (app.config, etc).</locDE>
		/// </summary>
		/// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
		/// <param name="value">The value to store.<locDE><para />Der zu speichernde Wert.</locDE></param>
		void SetValue(string key, string value);
        #endregion

        #region Flush
        /// <summary>
        /// Flushes unsaved changes (if any).
        /// <locDE><para />Stellt sicher, dass etwaige ungespeicherte Änderungen geschrieben werden.</locDE>
        /// </summary>
        void Flush();
        #endregion
    }
}
