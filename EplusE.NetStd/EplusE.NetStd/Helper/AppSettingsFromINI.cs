using System;

namespace EplusE
{
    /// <summary>
    /// Application settings access methods using INI file (app.ini).
    /// <locDE><para />Anwendungskonfiguration Zugriffsmethoden für eine INI-Datei (app.ini).</locDE>
    /// </summary>
    public class AppSettingsFromINI : EplusE.INI.INIBasis, IAppSettings
    {
        #region IniFilename
        private string _IniFilename = null;

        /// <summary>
        /// Gets or sets the INI filename.
        /// <locDE><para />Holt/setzt den INI-Dateinamen.</locDE>
        /// </summary>
        /// <value>The INI filename.<locDE><para />Der INI-Dateiname.</locDE></value>
        public string IniFilename
        {
            get
            {
                if (null == _IniFilename)
                {
                    if (!string.IsNullOrWhiteSpace(IniDatei))
                    {
                        _IniFilename = IniDatei;
                    }
                    else
                    {
                        _IniFilename = System.IO.Path.Combine(AppHelper.GetAppPath(), AppHelper.GetAppName() + ".ini");
                        IniDatei = _IniFilename;
                    }
                }
                return _IniFilename;
            }

            set
            {
                _IniFilename = value;
                IniDatei = _IniFilename;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsFromINI" /> class.
        /// <locDE><para />Initialisiert eine neue Instanz der Klasse <see cref="AppSettingsFromINI" />.</locDE>
        /// </summary>
        public AppSettingsFromINI()
        {
            //this.IniFilename = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsFromINI" /> class.
        /// <locDE><para />Initialisiert eine neue Instanz der Klasse <see cref="AppSettingsFromINI" />.</locDE>
        /// </summary>
        /// <param name="filename">The INI filename.<locDE><para />Der INI-Dateiname.</locDE></param>
        public AppSettingsFromINI(string filename)
        {
            this.IniFilename = filename;
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
            if (string.IsNullOrWhiteSpace(key))
                return defaultValue;

            string section = null;
            if (key.StartsWith("["))
                section = StringHelper.GetSubstringInsideOf("[", "]", ref key);
            if (string.IsNullOrWhiteSpace(section))
                section = "AppSettings";

            System.Text.StringBuilder sb = new System.Text.StringBuilder(4096);

            uint count = NativeMethods.GetPrivateProfileString(section, key, defaultValue ?? "", sb, (uint)sb.Capacity, this.IniFilename);
            if (count < 1)
                return defaultValue;

            return sb.ToString();
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

            string section = null;
            if (key.StartsWith("["))
                section = StringHelper.GetSubstringInsideOf("[", "]", ref key);
            if (string.IsNullOrWhiteSpace(section))
                section = "AppSettings";

            if (!NativeMethods.WritePrivateProfileString(section, key, value, this.IniFilename))
                throw new Exception("AppSettingsFromINI.SetValue: Could not write setting!");
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

        #region P/Invoke declarations
        // http://www.codeproject.com/Articles/20053/A-Complete-Win-INI-File-Utility-Class

        /// <summary>
        /// A static class that provides the win32 P/Invoke signatures 
        /// used by this class.
        /// </summary>
        /// <remarks>
        /// Note:  In each of the declarations below, we explicitly set CharSet to 
        /// Auto.  By default in C#, CharSet is set to Ansi, which reduces 
        /// performance on windows 2000 and above due to needing to convert strings
        /// from Unicode (the native format for all .Net strings) to Ansi before 
        /// marshalling.  Using Auto lets the marshaller select the Unicode version of 
        /// these functions when available.
        /// </remarks>
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class NativeMethods
        {
            //[System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            //public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer,
            //                                                       uint nSize,
            //                                                       string lpFileName);

            [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            public static extern uint GetPrivateProfileString(string lpAppName,
                                                              string lpKeyName,
                                                              string lpDefault,
                                                              System.Text.StringBuilder lpReturnedString,
                                                              uint nSize,
                                                              string lpFileName);

            //We explicitly enable the SetLastError attribute here because
            // WritePrivateProfileString returns errors via SetLastError.
            // Failure to set this can result in errors being lost during 
            // the marshal back to managed code.
            [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
            public static extern bool WritePrivateProfileString(string lpAppName,
                                                                string lpKeyName,
                                                                string lpString,
                                                                string lpFileName);
        }
        #endregion
    }
}
