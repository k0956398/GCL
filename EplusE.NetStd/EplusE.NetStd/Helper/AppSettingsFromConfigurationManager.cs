using System;

namespace EplusE
{
    /// <summary>
    /// Application settings access methods using ConfigurationManager (app.config).
    /// <locDE><para />Anwendungskonfiguration Zugriffsmethoden für den ConfigurationManager (app.config).</locDE>
    /// </summary>
    public class AppSettingsFromConfigurationManager : IAppSettings
    {
        #region AppName
        private string _AppName = null;

        /// <summary>
        /// Gets or sets the name of the application.
        /// <locDE><para />Holt/setzt den Namen der Anwendung.</locDE>
        /// </summary>
        /// <value>The name of the application.<locDE><para />Der Name der Anwendung.</locDE></value>
        public string AppName
        {
            get
            {
                if (null == _AppName)
                    _AppName = AppHelper.GetAppName();
                return _AppName;
            }

            set
            {
                _AppName = value;
            }
        }
        #endregion
        #region ConfigurationUserLevel
        /// <summary>
        /// Gets or sets the configuration user level.
        /// <locDE><para />Holt/setzt den ConfigurationUserLevel.</locDE>
        /// </summary>
        /// <value>The configuration user level.<locDE><para />Der ConfigurationUserLevel.</locDE></value>
        public System.Configuration.ConfigurationUserLevel ConfigurationUserLevel { get; set; }
        #endregion
        #region ConfigurationSaveMode
        /// <summary>
        /// Gets or sets the configuration save mode.
        /// <locDE><para />Holt/setzt den ConfigurationSaveMode.</locDE>
        /// </summary>
        /// <value>The configuration save mode.<locDE><para />Der ConfigurationSaveMode.</locDE></value>
        public System.Configuration.ConfigurationSaveMode ConfigurationSaveMode { get; set; }
        #endregion

        #region ConfigurationInstanceProvider
        /// <summary>
        /// Gets a specific Configuration instance. 
        /// I.e. for ASP.NET, return System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        /// <locDE><para />Holt eine spezifische Configuration-Instanz. 
        /// Z.B. für ASP.NET, System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~"); zurückliefern.</locDE>
        /// </summary>
        public Func<System.Configuration.Configuration> ConfigurationInstanceProvider = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsFromConfigurationManager" /> class.
        /// <locDE><para />Initialisiert eine neue Instanz der Klasse <see cref="AppSettingsFromConfigurationManager" />.</locDE>
        /// </summary>
        public AppSettingsFromConfigurationManager()
        {
            //this.AppName = null;
            this.ConfigurationUserLevel = System.Configuration.ConfigurationUserLevel.None;
            this.ConfigurationSaveMode = System.Configuration.ConfigurationSaveMode.Minimal;
        }
        #endregion

        #region GetValue
        /// <summary>
        /// Gets the value for the specified key from the persistent storage (app.config, etc).
        /// <locDE><para />Holt den Wert für den angegebenen Schlüsselbegriff aus dem Konfigurationsspeicher (app.config, etc).</locDE>
        /// </summary>
        /// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
        /// <param name="defaultValue">The default value.<locDE><para />Der Vorgabewert.</locDE></param>
        /// <returns>
        /// Loaded value or default value if not available.<locDE><para />Geladener Wert oder Vorgabewert falls nicht vorhanden.</locDE>
        /// </returns>
        public string GetValue(string key, string defaultValue = "")
        {
            string foundAt;
            return GetValue(key, out foundAt, defaultValue);
        }

        /// <summary>
        /// Gets the value for the specified key from the persistent storage (app.config, etc).
        /// <locDE><para />Holt den Wert für den angegebenen Schlüsselbegriff aus dem Konfigurationsspeicher (app.config, etc).</locDE>
        /// </summary>
        /// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
        /// <param name="foundAt">The found at identifier, i.e. userSettings, applicationSettings or appSettings.
        /// <locDE><para />Die Kennung der Fundstelle, z.B. userSettings, applicationSettings or appSettings.</locDE></param>
        /// <param name="defaultValue">The default value.<locDE><para />Der Vorgabewert.</locDE></param>
        /// <returns>
        /// Loaded value or default value if not available.<locDE><para />Geladener Wert oder Vorgabewert falls nicht vorhanden.</locDE>
        /// </returns>
        public string GetValue(string key, out string foundAt, string defaultValue = "")
        {
            foundAt = null;

            if (string.IsNullOrWhiteSpace(key))
                return defaultValue;

            #region TheAppName.Properties.Settings | userSettings
            string value = null;
            try
            {
                //<userSettings>
                //  <TheAppName.Properties.Settings>
                //    <setting name="TheKey" serializeAs="String">
                //      <value>TheValue</value>
                //    </setting>
                //  </TheAppName.Properties.Settings>
                //</userSettings>

                #region Get Configuration instance
                System.Configuration.Configuration config = null;
                if (null != ConfigurationInstanceProvider)
                    config = ConfigurationInstanceProvider();
                else
                    config = System.Configuration.ConfigurationManager.OpenExeConfiguration(this.ConfigurationUserLevel);
                #endregion

                System.Configuration.ClientSettingsSection clientSettingsSection =
                    config.GetSection("userSettings/" + this.AppName + ".Properties.Settings") as System.Configuration.ClientSettingsSection;
                if (null != clientSettingsSection)
                {
                    // Try to get existing entry
                    System.Configuration.SettingElement settingElement = clientSettingsSection.Settings.Get(key);
                    if (null != settingElement)
                    {
                        value = (settingElement.Value.ValueXml).LastChild.InnerText.ToString();

                        foundAt = "userSettings";

                        if (string.IsNullOrEmpty(value))
                            return defaultValue;
                        return value;
                    }
                }
            }
            catch (Exception ex) { HandleException(ex); }
            #endregion

            #region TheAppName.Properties.Settings | applicationSettings
            try
            {
                //<applicationSettings>
                //  <TheAppName.Properties.Settings>
                //    <setting name="TheKey" serializeAs="String">
                //      <value>TheValue</value>
                //    </setting>
                //  </TheAppName.Properties.Settings>
                //</applicationSettings>

                #region Get Configuration instance
                System.Configuration.Configuration config = null;
                if (null != ConfigurationInstanceProvider)
                    config = ConfigurationInstanceProvider();
                else
                    config = System.Configuration.ConfigurationManager.OpenExeConfiguration(this.ConfigurationUserLevel);
                #endregion

                System.Configuration.ClientSettingsSection clientSettingsSection =
                    config.GetSection("applicationSettings/" + this.AppName + ".Properties.Settings") as System.Configuration.ClientSettingsSection;
                if (null != clientSettingsSection)
                {
                    // Try to get existing entry
                    System.Configuration.SettingElement settingElement = clientSettingsSection.Settings.Get(key);
                    if (null != settingElement)
                    {
                        value = (settingElement.Value.ValueXml).LastChild.InnerText.ToString();

                        foundAt = "applicationSettings";

                        if (string.IsNullOrEmpty(value))
                            return defaultValue;
                        return value;
                    }
                }
            }
            catch (Exception ex) { HandleException(ex); }
            #endregion

            #region appSettings
            try
            {
                //<appSettings>
                //  <add key="TheKey" value="TheValue" />
                //</appSettings>
                value = System.Configuration.ConfigurationManager.AppSettings[key];
            }
            catch { }

            foundAt = "appSettings";

            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value;
            #endregion
        }
        #endregion
        #region SetValue
        /// <summary>
        /// Sets the value for the specified key in the persistent storage (app.config, etc).
        /// <locDE><para />Schreibt den Wert für den angegebenen Schlüsselbegriff in den Konfigurationsspeicher (app.config, etc).</locDE>
        /// </summary>
        /// <param name="key">The key.<locDE><para />Der Schlüsselbegriff.</locDE></param>
        /// <param name="value">The value to store.<locDE><para />Der zu speichernde Wert.</locDE></param>
        public void SetValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;

            string writeInto = null;

            if (key.EndsWith("@appSettings"))
                writeInto = "appSettings";
            else if (key.EndsWith("@applicationSettings"))
                writeInto = "applicationSettings";
            else if (key.EndsWith("@userSettings"))
                writeInto = "userSettings";
            else
            {
                GetValue(key, out writeInto);
            }

            #region TheAppName.Properties.Settings | userSettings
            if (writeInto.Equals("userSettings"))
            {
                try
                {
                    //<userSettings>
                    //  <TheAppName.Properties.Settings>
                    //    <setting name="TheKey" serializeAs="String">
                    //      <value>TheValue</value>
                    //    </setting>
                    //  </TheAppName.Properties.Settings>
                    //</userSettings>

                    #region Get Configuration instance
                    System.Configuration.Configuration config = null;
                    if (null != ConfigurationInstanceProvider)
                        config = ConfigurationInstanceProvider();
                    else
                        config = System.Configuration.ConfigurationManager.OpenExeConfiguration(this.ConfigurationUserLevel);
                    #endregion

                    System.Configuration.ClientSettingsSection clientSettingsSection =
                        config.GetSection("userSettings/" + this.AppName + ".Properties.Settings") as System.Configuration.ClientSettingsSection;
                    if (null != clientSettingsSection)
                    {
                        // Try to get existing entry
                        System.Configuration.SettingElement settingElement = clientSettingsSection.Settings.Get(key);
                        if (null != settingElement)
                        {
                            // Remove existing entry
                            clientSettingsSection.Settings.Remove(settingElement);
                            // Set new value
                            settingElement.Value.ValueXml.InnerXml = value;
                            clientSettingsSection.Settings.Add(settingElement);
                        }
                        else
                        {
                            // Create new entry
                            settingElement = new System.Configuration.SettingElement(key, System.Configuration.SettingsSerializeAs.String);
                            settingElement.Value = new System.Configuration.SettingValueElement();
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            settingElement.Value.ValueXml = doc.CreateElement("value");
                            // Set new value
                            settingElement.Value.ValueXml.InnerXml = value;
                            clientSettingsSection.Settings.Add(settingElement);
                        }
                        // Save changes
                        config.Save(this.ConfigurationSaveMode);
                        System.Configuration.ConfigurationManager.RefreshSection("userSettings");
                    }
                }
                catch (Exception ex) { HandleException(ex); }
                return;
            }
            #endregion

            #region TheAppName.Properties.Settings | applicationSettings
            if (writeInto.Equals("applicationSettings"))
            {
                try
                {
                    //<applicationSettings>
                    //  <TheAppName.Properties.Settings>
                    //    <setting name="TheKey" serializeAs="String">
                    //      <value>TheValue</value>
                    //    </setting>
                    //  </TheAppName.Properties.Settings>
                    //</applicationSettings>

                    #region Get Configuration instance
                    System.Configuration.Configuration config = null;
                    if (null != ConfigurationInstanceProvider)
                        config = ConfigurationInstanceProvider();
                    else
                        config = System.Configuration.ConfigurationManager.OpenExeConfiguration(this.ConfigurationUserLevel);
                    #endregion

                    System.Configuration.ClientSettingsSection clientSettingsSection =
                        config.GetSection("applicationSettings/" + this.AppName + ".Properties.Settings") as System.Configuration.ClientSettingsSection;
                    if (null != clientSettingsSection)
                    {
                        // Try to get existing entry
                        System.Configuration.SettingElement settingElement = clientSettingsSection.Settings.Get(key);
                        if (null != settingElement)
                        {
                            // Remove existing entry
                            clientSettingsSection.Settings.Remove(settingElement);
                            // Set new value
                            settingElement.Value.ValueXml.InnerXml = value;
                            clientSettingsSection.Settings.Add(settingElement);
                        }
                        else
                        {
                            // Create new entry
                            settingElement = new System.Configuration.SettingElement(key, System.Configuration.SettingsSerializeAs.String);
                            settingElement.Value = new System.Configuration.SettingValueElement();
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            settingElement.Value.ValueXml = doc.CreateElement("value");
                            // Set new value
                            settingElement.Value.ValueXml.InnerXml = value;
                            clientSettingsSection.Settings.Add(settingElement);
                        }
                        // Save changes
                        config.Save(this.ConfigurationSaveMode);
                        System.Configuration.ConfigurationManager.RefreshSection("applicationSettings");
                    }
                }
                catch (Exception ex) { HandleException(ex); }
                return;
            }
            #endregion

            #region appSettings
            //if (writeInto.Equals("appSettings"))
            {
                //<appSettings>
                //  <add key="TheKey" value="TheValue" />
                //</appSettings>

                // Does not save the new value into app.config file (only changed for app runtime):
                //System.Configuration.ConfigurationManager.AppSettings[key] = value;

                #region Get Configuration instance
                System.Configuration.Configuration config = null;
                if (null != ConfigurationInstanceProvider)
                    config = ConfigurationInstanceProvider();
                else
                    config = System.Configuration.ConfigurationManager.OpenExeConfiguration(this.ConfigurationUserLevel);
                #endregion

                if (null == config.AppSettings.Settings[key])
                    config.AppSettings.Settings.Add(key, value);
                else
                    config.AppSettings.Settings[key].Value = value;

                config.Save(this.ConfigurationSaveMode);
                System.Configuration.ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
                //return;
            }
            #endregion
        }
        #endregion
        #region Flush
        /// <summary>
        /// Flushes unsaved changes (if any).
        /// <locDE><para />Stellt sicher, dass etwaige ungespeicherte Änderungen geschrieben werden.</locDE>
        /// </summary>
        public void Flush()
        {
            // nothing to do
        }
        #endregion

        #region HandleException
        /// <summary>
        /// Handles the exception.
        /// <locDE><para />Behandelt eine aufgetretene Exception.</locDE>
        /// </summary>
        /// <param name="ex">The exception.<locDE><para />Die Exception.</locDE></param>
        private void HandleException(Exception ex)
        {
            // http://www.codeproject.com/Articles/30216/Handling-Corrupt-user-config-Settings

            //[2016-05-09 09:55:42] - HandleError: System.Configuration.ConfigurationErrorsException: Configuration system failed to initialize
            //System.Configuration.ConfigurationErrorsException: Root element is missing. 
            //   (C:\Users\at3056\AppData\Local\E+E_Elektronik_Ges.m.b.H.\EEHx.exe_Url_tqfckyusmiilpyk5noxyjzp5btecjoci\1.1.0.0\user.config)
            //System.Xml.XmlException: Root element is missing.
            //Stack Trace:    at System.Configuration.ConfigurationManager.PrepareConfigSystem()
            //   at System.Configuration.ConfigurationManager.get_AppSettings()
            //   at EplusE.AppSettingsFromConfigurationManager.GetValue(String key, String& foundAt, String defaultValue)

            if (ex is System.Configuration.ConfigurationErrorsException)
            {
                string filename = ((System.Configuration.ConfigurationErrorsException)ex.InnerException).Filename;

                //if (MessageBox.Show("<ProgramName> has detected that your" +
                //                      " user settings file has become corrupted. " +
                //                      "This may be due to a crash or improper exiting" +
                //                      " of the program. <ProgramName> must reset your " +
                //                      "user settings in order to continue.\n\nClick" +
                //                      " Yes to reset your user settings and continue.\n\n" +
                //                      "Click No if you wish to attempt manual repair" +
                //                      " or to rescue information before proceeding.",
                //                      "Corrupt user settings",
                //                      MessageBoxButton.YesNo,
                //                      MessageBoxImage.Error) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(filename) && System.IO.File.Exists(filename))
                            System.IO.File.Delete(filename);
                    }
                    catch { }
                    // you could optionally restart the app instead
                    //Settings.Default.Reload();
                }
                //else
                //{
                //    // avoid the inevitable crash
                //    System.Diagnostics.Process.GetCurrentProcess().Kill();
                //}
            }
        }
        #endregion
    }
}
