using System;

namespace EplusE
{
    /// <summary>
    /// Provides application settings (app.config, etc.) access methods.
    /// <locDE><para />Stellt Methoden zum Zugriff auf die Anwendungskonfiguration (app.config, etc.) bereit.</locDE>
    /// </summary>
    public static class AppSettings
    {
        #region Event ProviderNeeded
        /// <summary>
        /// Handler for ProviderNeeded event.
        /// <locDE><para />Ereignismethode für ProviderNeeded.</locDE>
        /// </summary>
        public delegate IAppSettings ProviderNeededHandler();
        /// <summary>
        /// Occurs when application settings (app.config, etc.) access is needed (request injection of access provider instance).
        /// <locDE><para />Tritt auf, wenn Zugriff auf die Anwendungskonfiguration (app.config, etc.) benötigt wird (Anforderung für Injektion einer Zugriffsprovider-Instanz).</locDE>
        /// </summary>
        public static event ProviderNeededHandler ProviderNeeded;

        /// <summary>
        /// Fire event ProviderNeeded.
        /// <locDE><para />Ereignis ProviderNeeded auslösen.</locDE>
        /// </summary>
        private static IAppSettings OnProviderNeeded()
        {
            var tempEvent = ProviderNeeded;
            if (null != tempEvent)
                return tempEvent();
            return null;
        }
        #endregion
        #region AppSettingsProvider
        private static IAppSettings _AppSettingsProvider = null;

        /// <summary>
        /// Gets or sets the application settings provider instance.
        /// <locDE><para />Holt/setzt die Anwendungskonfiguration-Zugriffsprovider-Instanz.</locDE>
        /// </summary>
        /// <value>The application settings provider instance.<locDE><para />Die Anwendungskonfiguration-Zugriffsprovider-Instanz.</locDE></value>
        private static IAppSettings AppSettingsProvider
        {
            get
            {
                if (null == _AppSettingsProvider)
                {
                    _AppSettingsProvider = OnProviderNeeded();

                    // Last resort fallback: Use app.config (on Windows)
                    if (null == _AppSettingsProvider)
                        _AppSettingsProvider = new AppSettingsFromConfigurationManager();
                }
                return _AppSettingsProvider;
            }

            set
            {
                _AppSettingsProvider = value;
            }
        }
        #endregion

        #region GetValue
        /// <summary>
        /// Gets the value for the specified key from the persistent storage (app.config, etc).
        /// <locDE><para />Holt den Wert für den angegebenen Schlüsselbegriff aus dem Konfigurationsspeicher (app.config, etc).</locDE>
        /// </summary>
        /// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
        /// <param name="defaultValue">The default value.<locDE><para />Der Vorgabewert.</locDE></param>
        /// <returns>Loaded value or default value if not available.<locDE><para />Geladener Wert oder Vorgabewert falls nicht vorhanden.</locDE></returns>
        public static string GetValue(string key, string defaultValue = "")
        {
            return AppSettingsProvider.GetValue(key, defaultValue);
        }
        #endregion
        #region SetValue
        /// <summary>
        /// Sets the value for the specified key in the persistent storage (app.config, etc).
        /// <locDE><para />Schreibt den Wert für den angegebenen Schlüsselbegriff in den Konfigurationsspeicher (app.config, etc).</locDE>
        /// </summary>
        /// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
        /// <param name="value">The value to store.<locDE><para />Der zu speichernde Wert.</locDE></param>
        public static void SetValue(string key, string value)
        {
            AppSettingsProvider.SetValue(key, value);
        }
        #endregion
        #region Flush
        /// <summary>
        /// Flushes unsaved changes (if any).
        /// <locDE><para />Stellt sicher, dass etwaige ungespeicherte Änderungen geschrieben werden.</locDE>
        /// </summary>
        public static void Flush()
        {
            AppSettingsProvider.Flush();
        }
        #endregion
    }
}
