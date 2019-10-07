using System;
using System.Linq;

namespace EplusE
{
    /// <summary>
    /// Provides application wide helper methods.
    /// <locDE><para />Stellt anwendungsweite Hilfsmethoden bereit.</locDE>
    /// </summary>
    public static partial class AppHelper
    {
        #region EntryAssembly
        private static System.Reflection.Assembly _EntryAssembly = null;
        /// <summary>
        /// Gets or sets the entry assembly.
        /// <locDE><para />Setzt/holt die Einstiegs-Assembly.</locDE>
        /// </summary>
        /// <value>
        /// The entry assembly.
        /// <locDE><para />Die Einstiegs-Assembly.</locDE>
        /// </value>
        public static System.Reflection.Assembly EntryAssembly
        {
            get
            {
                return _EntryAssembly;
            }

            set
            {
                if (_EntryAssembly != value)
                {
                    _EntryAssembly = value;
                }
            }
        }
        #endregion

        #region Static constructor
        /// <summary>
        /// List of IDE path suffixes, i.e. "\\bin\\debug".
        /// <locDE><para />Liste der Entwicklungspfad-Endungen, z.B. "\\bin\\debug".</locDE>
        /// </summary>
        public static System.Collections.Generic.IList<string> IDEPathSuffixes = null;

        /// <summary>
        /// Static constructor.
        /// <locDE><para />Statischer Konstruktor.</locDE>
        /// </summary>
        static AppHelper()
        {
            IDEPathSuffixes = new System.Collections.Generic.List<string>();
            IDEPathSuffixes.Add("\\bin\\debug");
            IDEPathSuffixes.Add("\\bin\\release");

            // Assignment will be valid for console or WPF applications, null for ASP.net (must be injected)
            // Zuweisung wird gültig sein bei Konsolen- oder WPF-Anwendung, null bei ASP.net (muss injiziert werden)
            EntryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (null == EntryAssembly)
            {
                // Most probably ASP.net environment
                // Höchstwahrscheinlich ASP.net Umgebung
                RuntimeHelper.IsRunningOnASPnet = true;
            }
        }
        #endregion

        #region App uptime/timestamp methods        
        /// <summary>
        /// The application start up date time (UTC).
        /// <locDE><para />Die Anwendungsstartzeit (UTC).</locDE>
        /// </summary>
        private static DateTime _StartUpDateTimeUtc = DateTime.UtcNow;

        /// <summary>
        /// Gets the application start date time (UTC).
        /// <locDE><para />Liefert die Anwendungsstartzeit (UTC).</locDE>
        /// </summary>
        /// <returns>The application start date time (UTC).
        /// <locDE><para />Die Anwendungsstartzeit (UTC).</locDE>
        /// </returns>
        public static DateTime GetAppStartDateTime()
        {
            return _StartUpDateTimeUtc;
        }

        /// <summary>
        /// Gets the application uptime (seconds).
        /// <locDE><para />Liefert die Anwendungs-Uptime (Sekunden).</locDE>
        /// </summary>
        /// <returns>The application uptime (seconds).
        /// <locDE><para />Die Anwendungs-Uptime (Sekunden).</locDE>
        /// </returns>
        public static UInt32 GetAppUpTimeSecs()
        {
            return (UInt32)(DateTime.UtcNow - _StartUpDateTimeUtc).TotalSeconds;
        }

        /// <summary>
        /// Gets the epoch timestamp in UTC (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Liefert den UNIX Epochen-Zeitstempel (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <returns>The epoch timestamp in UTC (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Der UNIX Epochen-Zeitstempel (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </returns>
        public static UInt64 GetEpochTimestampUTC()
        {
            return DateTimeHelper.GetEpochTimestampFromUTC(DateTime.UtcNow);
        }

        /// <summary>
        /// Gets the epoch timestamp in local time (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Liefert den UNIX Epochen-Zeitstempel als Lokalzeit (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </summary>
        /// <returns>The epoch timestamp in local time (milliseconds since 1970-01-01 UTC).
        /// <locDE><para />Der UNIX Epochen-Zeitstempel als Lokalzeit (Millisekunden seit 1970-01-01 UTC).</locDE>
        /// </returns>
        public static UInt64 GetEpochTimestampLocalTime()
        {
            // Simulate UTC, as otherwise the result would be the same as of GetEpochTimestampUTC()
            return DateTimeHelper.GetEpochTimestampFromUTC(DateTime.Now);
        }
        #endregion

        #region Computer name
        /// <summary>
        /// Gets the computer/host name.
        /// <locDE><para />Liefert den Computernamen (Host name).</locDE>
        /// </summary>
        /// <returns>
        /// The computer/host name.
        /// <locDE><para />Der Computername (Host name).</locDE>
        /// </returns>
        public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }
        #endregion

        #region App name methods
        private static string _AppName = null;

        /// <summary>
        /// Gets the application name. Automatically initialized to the entry assembly name (exe).
        /// <locDE><para />Liefert den Anwendungsnamen. Automatisch initialisiert mit dem Namen der Startassembly (exe).</locDE>
        /// </summary>
        /// <returns>
        /// The application name. Automatically initialized to the entry assembly name (exe).
        /// <locDE><para />Der Anwendungsname. Automatisch initialisiert mit dem Namen der Startassembly (exe).</locDE>
        /// </returns>
        public static string GetAppName()
        {
            if (null == _AppName)
            {
                try
                {
                    System.Reflection.Assembly assembly = EntryAssembly;
                    if (null != assembly)
                        _AppName = assembly.GetName().Name;
                }
                catch
                {
                    _AppName = "Unknown";
                }
            }
            return _AppName;
        }

        /// <summary>
        /// Sets the application name.
        /// <locDE><para />Setzt den Anwendungsnamen.</locDE>
        /// </summary>
        /// <param name="appName">Name of the application.<locDE><para />Name der Anwendung.</locDE></param>
        public static void SetAppName(string appName)
        {
            _AppName = appName ?? "";
        }
        #endregion

        #region App version methods
        private static Version _AppVersion = null;

        /// <summary>
        /// Gets the application version.
        /// <locDE><para />Liefert die Anwendungsversion.</locDE>
        /// </summary>
        /// <returns>
        /// The application version.
        /// <locDE><para />Die Anwendungsversion.</locDE>
        /// </returns>
        public static Version GetAppVersion()
        {
            if (null == _AppVersion)
            {
                System.Reflection.Assembly assembly = EntryAssembly;
                if (null != assembly)
                    _AppVersion = assembly.GetName().Version;
            }
            return _AppVersion;
        }

        /// <summary>
        /// Gets the application version as formatted string, typically "V2.0".
        /// <locDE><para />Liefert die Anwendungsversion als formatierten String, typischerweise "V2.0".</locDE>
        /// </summary>
        /// <param name="prefix">The prefix string, i.e. "V".<locDE><para />Das Präfix, z.B. "V".</locDE></param>
        /// <param name="fieldCount">The field count (1 = only major, 2 = major + minor, etc).<locDE><para />Die Feldanzahl (1 = nur Hauptversion, 2 = Haupt- + Nebenversion, etc).</locDE></param>
        /// <returns>Formatted version string.<locDE><para />Formatierter Versions-String.</locDE></returns>
        public static string GetAppVersionString(string prefix = "V", int fieldCount = 2)
        {
            return GetAppVersionString(prefix, fieldCount, false);
        }

        /// <summary>
        /// Gets the application version as formatted string, typically "V2.0".
        /// <locDE><para />Liefert die Anwendungsversion als formatierten String, typischerweise "V2.0".</locDE>
        /// </summary>
        /// <param name="prefix">The prefix string, i.e. "V".<locDE><para />Das Präfix, z.B. "V".</locDE></param>
        /// <param name="fieldCount">The field count (1 = only major, 2 = major + minor, etc).<locDE><para />Die Feldanzahl (1 = nur Hauptversion, 2 = Haupt- + Nebenversion, etc).</locDE></param>
        /// <param name="includeNonZeroFields">If true, non-zero fields beyond <paramref name="fieldCount" /> are also included (i.e. non-zero build/revision field although <paramref name="fieldCount" /> is 2).
        /// <locDE><para />Falls true, werden auch Felder über den <paramref name="fieldCount" /> hinaus geliefert, so sie einen Wert ungleich 0 enthalten (z.B. Build/Revision Nummer ungleich 0, obwohl <paramref name="fieldCount" /> gleich 2 ist).</locDE></param>
        /// <returns>Formatted version string.<locDE><para />Formatierter Versions-String.</locDE></returns>
        public static string GetAppVersionString(string prefix, int fieldCount, bool includeNonZeroFields)
        {
            return GetAppVersion().ToString(prefix, fieldCount, includeNonZeroFields);
        }
        #endregion

        #region App copyright method
        private static string _AppCopyright = null;

        /// <summary>
        /// Gets the application copyright message.
        /// <locDE><para />Liefert das Anwendungscopyright.</locDE>
        /// </summary>
        /// <returns>
        /// The application copyright message.
        /// <locDE><para />Das Anwendungscopyright.</locDE>
        /// </returns>
        public static string GetAppCopyright()
        {
            if (null == _AppCopyright)
            {
                try
                {
                    System.Reflection.Assembly assembly = EntryAssembly;
                    if (null != assembly)
                        _AppCopyright = ((System.Reflection.AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly,
                            typeof(System.Reflection.AssemblyCopyrightAttribute), false)).Copyright;
                }
                catch
                {
                    //_AppCopyright = "Copyright (c) " + DateTime.Now.Year;
                    _AppCopyright = "";
                }
            }
            return _AppCopyright;
        }
        #endregion

        #region App status methods
        private static bool _IsShuttingDown = false;

        /// <summary>
        /// Determines whether the application is shutting down.
        /// <locDE><para />Ermittelt, ob die Anwendung gerade heruntergefahren wird.</locDE>
        /// </summary>
        /// <returns>
        /// Is the application shutting down?
        /// <locDE><para />Wird die Anwendung gerade heruntergefahren?</locDE>
        /// </returns>
        public static bool IsShuttingDown()
        {
            return _IsShuttingDown;
        }

        /// <summary>
        /// Sets the is shutting down flag.
        /// <locDE><para />Setzt das Anwendung fährt herunter Flag.</locDE>
        /// </summary>
        public static void SetIsShuttingDown()
        {
            if (!_IsShuttingDown)
            {
                _IsShuttingDown = true;
            }
        }
        #endregion

        #region Filesystem methods (Paths, temporary filename, filename validity, etc).
        #region EnsureAbsolutePath
        /// <summary>
        /// Ensures an absolute path (might be given as (app) relative).
        /// <locDE><para />Stellt einen absoluten Pfad sicher. Kann relativ zur Anwendung angegeben werden.</locDE>
        /// </summary>
        /// <param name="potentiallyRelativePath">The absolute or (app) relative path.<locDE><para />Der absolute oder anwendungsrelative Pfad.</locDE></param>
        /// <param name="basePath">The base path, null means application base path.<locDE><para />Basispfad, null bedeutet Anwendungsbasispfad.</locDE></param>
        /// <returns>Absolute path.<locDE><para />Absoluter Pfad.</locDE></returns>
        public static string EnsureAbsolutePath(string potentiallyRelativePath, string basePath = null)
        {
            if (string.IsNullOrWhiteSpace(potentiallyRelativePath))
                return null;
            if (null == basePath)
                basePath = GetAppPath();

            if (!System.IO.Path.IsPathRooted(potentiallyRelativePath))
            {
                // Seems to be a relative path, make absolute
                potentiallyRelativePath = System.IO.Path.Combine(basePath, potentiallyRelativePath);
                potentiallyRelativePath = System.IO.Path.GetFullPath(potentiallyRelativePath);
            }
            return potentiallyRelativePath;
        }
        #endregion

        #region EnsureAbsoluteFilename
        /// <summary>
        /// Ensures an absolute filename (might be given as (app) relative).
        /// <locDE><para />Stellt einen absoluten Dateinamen sicher. Kann relativ zur Anwendung angegeben werden.</locDE>
        /// </summary>
        /// <param name="potentiallyRelativeFilename">The absolute or (app) relative filename.<locDE><para />Der absolute oder anwendungsrelative Dateiname.</locDE></param>
        /// <param name="basePath">The base path, null means application base path.<locDE><para />Basispfad, null bedeutet Anwendungsbasispfad.</locDE></param>
        /// <returns>Absolute filename.<locDE><para />Absoluter Dateiname.</locDE></returns>
        public static string EnsureAbsoluteFilename(string potentiallyRelativeFilename, string basePath = null)
        {
            if (string.IsNullOrWhiteSpace(potentiallyRelativeFilename))
                return null;
            if (null == basePath)
                basePath = GetAppPath();

            if (!System.IO.Path.IsPathRooted(potentiallyRelativeFilename))
            {
                // Seems to be a relative filename, make absolute
                potentiallyRelativeFilename = System.IO.Path.Combine(basePath, potentiallyRelativeFilename);
                potentiallyRelativeFilename = System.IO.Path.GetFullPath(potentiallyRelativeFilename);
            }
            return potentiallyRelativeFilename;
        }
        #endregion

        #region GetAppPath
        private static string _AppPath = null;

        /// <summary>
        /// Overrides the application path (set without trailing backslash).
        /// <locDE><para />Übersteuert den Anwendungspfad (ohne abschließenden Backslash setzen).</locDE>
        /// </summary>
        /// <param name="path">The path without trailing backslash.
        /// <locDE><para />Der Anwendungspfad ohne abschließendem Backslash.</locDE>
        /// </param>
        public static void OverrideAppPath(string path)
        {
            _AppPath = path;
        }

        /// <summary>
        /// Gets the application base directory (path) without trailing backslash.
        /// <locDE><para />Liefert den Anwendungsbasispfad ohne abschließendem Backslash.</locDE>
        /// </summary>
        /// <returns>
        /// The application base directory (path) without trailing backslash.
        /// <locDE><para />Der Anwendungsbasispfad ohne abschließendem Backslash.</locDE>
        /// </returns>
        public static string GetAppPath()
        {
            if (null != _AppPath)
                return _AppPath;

            string baseDir = null;
            if (RuntimeHelper.IsDesignTime)
            {
                // No design time support yet...
                return null;
            }
            else
            {
                // NOTE: GetExecutingAssembly() does not work as expected with ASP.NET
                //baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                baseDir = AppDomain.CurrentDomain.BaseDirectory;
            }
            baseDir = baseDir.TrimEnd('\\');

            bool idePathsFound = false;  // directory structure of IDE (Visual Studio) detected?
            if (RuntimeHelper.IsDesignTimeOrInsideIDE)
            {
                if (null != IDEPathSuffixes)
                {
                    // Remove IDE path suffixes like "\\bin\\debug"
                    foreach (string idePathSuffix in IDEPathSuffixes)
                    {
                        if (baseDir.ToLower().EndsWith(idePathSuffix.ToLower()))
                        {
                            baseDir = baseDir.Substring(0, baseDir.Length - idePathSuffix.Length);
                            idePathsFound = true;
                        }
                    }
                }
            }
            if (idePathsFound || RuntimeHelper.IsRunningOnASPnet)
            {
                // Remove subdir of main assembly, reaching directory level of .sln file
                string oneUp = System.IO.Path.GetDirectoryName(baseDir);
                if (AnyFileExists(oneUp, "*.sln"))
                    baseDir = oneUp;
            }
            _AppPath = baseDir;
            return _AppPath;
        }
        #endregion

        #region GetAppRelativePath
        /// <summary>
        /// Gets the app relative path.
        /// <locDE><para />Liefert den Pfad relativ zur Anwendung.</locDE>
        /// </summary>
        /// <param name="subDirName">Name of the sub directory.<locDE><para />Name des Unterverzeichnisses.</locDE></param>
        /// <param name="subDirsBelow">Optional: Sub directories below <paramref name="subDirName"/> or null.
        /// <locDE><para />Optional: Unterverzeichnisse innerhalb <paramref name="subDirName"/> oder null.</locDE></param>
        /// <param name="ensurePathExists">Should the directory structure be created if not existing?
        /// <locDE><para />Soll die Verzeichnisstruktur erzeugt werden falls noch nicht vorhanden?</locDE></param>
        /// <returns>The app relative path.
        /// <locDE><para />Der Pfad relativ zur Anwendung.</locDE>
        /// </returns>
        public static string GetAppRelativePath(string subDirName, string subDirsBelow = null, bool ensurePathExists = false)
        {
            string fullPath = System.IO.Path.Combine(GetAppPath(), subDirName);
            if (null != subDirsBelow)
                fullPath = System.IO.Path.Combine(fullPath, subDirsBelow);

            #region Support directory redirection file
            if (!System.IO.Directory.Exists(fullPath))
            {
                // I.e. path "c:\a\b\c\d" should be fetched 
                //   -> if not existing, check for file "c:\a\b\c\d.redirect" containing the path to use instead
                string fullPathOneUp = System.IO.Path.GetFullPath(System.IO.Path.Combine(fullPath, ".."));
                string redirectFile = fullPath + ".redirect";
                if (System.IO.File.Exists(redirectFile))
                {
                    // Directory redirection file exists, follow it
                    try
                    {
                        string redirectPath = System.IO.File.ReadAllText(redirectFile).Trim(new char[] { ' ', '\t', '\r', '\n', '"', '\'' });
                        fullPath = EnsureAbsolutePath(redirectPath, fullPathOneUp);
                    }
                    catch (Exception ex)
                    {
                        EplusE.Log.AppLogger.Log(ex);
                    }
                }
            }
            #endregion

            if (ensurePathExists)
                EnsureDirectoryExists(fullPath);

            return fullPath;
        }
        #endregion

        #region GetContentPath
        private static string _ContentPath = null;

        /// <summary>
        /// Overrides the content path (set without trailing backslash).
        /// <locDE><para />Übersteuert den Contentpfad (ohne abschließenden Backslash setzen).</locDE>
        /// </summary>
        /// <param name="path">The path without trailing backslash.
        /// <locDE><para />Der Contentpfad ohne abschließendem Backslash.</locDE>
        /// </param>
        public static void OverrideContentPath(string path)
        {
            _ContentPath = path;
        }

        /// <summary>
        /// Gets the content base directory (path) without trailing backslash. Useful i.e. with ASP.NET.
        /// <locDE><para />Liefert den Contentbasispfad ohne abschließendem Backslash. Nützlich z.B. bei ASP.NET.</locDE>
        /// </summary>
        /// <returns>
        /// The content base directory (path) without trailing backslash.
        /// <locDE><para />Der Contentbasispfad ohne abschließendem Backslash.</locDE>
        /// </returns>
        public static string GetContentPath()
        {
            if (null != _ContentPath)
                return _ContentPath;

            string baseDir = null;
            if (RuntimeHelper.IsRunningOnASPnet)
            {
                // NOTE: GetExecutingAssembly() does not work as expected with ASP.NET
                //baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                baseDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            }
            else
            {
                baseDir = GetAppPath();
            }

            _ContentPath = baseDir;
            return _ContentPath;
        }
        #endregion

        #region GetTempFilesPath
        private static string _tempPath_MayBeLocal = null;
        private static string _tempPath_MustBeLocal = null;

        /// <summary>
        /// App default path to place temporary files (should be empty on app exit).
        /// <locDE><para />Standardpfad für temporäre Dateien (soll bei Anwendungsende leer sein).</locDE>
        /// </summary>
        /// <param name="mustBeLocal">Is the temporary files path required to be on a local drive (no network drive)?
        /// <locDE><para />Muss das Verzeichnis für temporäre Dateien auf einem lokalen Laufwerk sein (kein Netzlaufwerk)?</locDE></param>
        /// <returns>App default path to place temporary files.
        /// <locDE><para />Verzeichnis für temporäre Dateien.</locDE>
        /// </returns>
        public static string GetTempFilesPath(bool mustBeLocal = false)
        {
            string _tempPath = mustBeLocal ? _tempPath_MustBeLocal : _tempPath_MayBeLocal;

            if (null == _tempPath)
            {
                try
                {
                    _tempPath = AppSettings.GetValue("TempFilesPath");
                    if (!string.IsNullOrWhiteSpace(_tempPath))
                        _tempPath = Environment.ExpandEnvironmentVariables(_tempPath);  // resolve %TEMP%, %WINDIR%, %SYSTEMROOT%, etc.
                    if (!string.IsNullOrWhiteSpace(_tempPath) &&
                        (!mustBeLocal || IsLocalPath(_tempPath)) &&
                        EnsureDirectoryExists(_tempPath) &&
                        CheckDirectoryWriteAccess(_tempPath))
                    {
                        if (mustBeLocal)
                            _tempPath_MustBeLocal = _tempPath;
                        else
                            _tempPath_MayBeLocal = _tempPath;
                        return _tempPath;
                    }
                }
                catch { }

                try
                {
                    _tempPath = GetAppRelativePath("Temp", null, false);
                    if (!string.IsNullOrWhiteSpace(_tempPath) &&
                        (!mustBeLocal || IsLocalPath(_tempPath)) &&
                        EnsureDirectoryExists(_tempPath) &&
                        CheckDirectoryWriteAccess(_tempPath))
                    {
                        if (mustBeLocal)
                            _tempPath_MustBeLocal = _tempPath;
                        else
                            _tempPath_MayBeLocal = _tempPath;
                        return _tempPath;
                    }
                }
                catch { }

                try
                {
                    _tempPath = System.IO.Path.GetTempPath();
                    if (!string.IsNullOrWhiteSpace(_tempPath) &&
                        //(!mustBeLocal || IsLocalPath(_tempPath)) &&
                        EnsureDirectoryExists(_tempPath) &&
                        CheckDirectoryWriteAccess(_tempPath))
                    {
                        if (mustBeLocal)
                            _tempPath_MustBeLocal = _tempPath;
                        else
                            _tempPath_MayBeLocal = _tempPath;
                        return _tempPath;
                    }
                }
                catch { }

                // No writeable directory for temporary files?!?
                _tempPath = null;
                throw new ArgumentException("No writeable directory for temporary files?!?");
            }
            return _tempPath;
        }

        /// <summary>
        /// Get path with randomly named subdir to place temporary files (should be empty on app exit).
        /// <locDE><para />Ermittelt einen Verzeichnisnamen mit zufälligem Unterverzeichnis zur Ablage von temporären Dateien (soll bei Anwendungsende leer sein).</locDE>
        /// </summary>
        /// <param name="mustBeLocal">Is the temporary files path required to be on a local drive (no network drive)?
        /// <locDE><para />Muss das Verzeichnis für temporäre Dateien auf einem lokalen Laufwerk sein (kein Netzlaufwerk)?</locDE></param>
        /// <returns>Path to place temporary files.
        /// <locDE><para />Verzeichnis für temporäre Dateien.</locDE>
        /// </returns>
        public static string GetTempFilesPathWithRandomSubDir(bool mustBeLocal = false)
        {
            string basePath = GetTempFilesPath(mustBeLocal);

            if (string.IsNullOrWhiteSpace(basePath))
                return null;

            while (true)
            {
                string path = System.IO.Path.Combine(basePath, GetAppName() + "_" + Guid.NewGuid().ToString().Trim('{', '}').Replace("-", ""));
                if (!System.IO.Directory.Exists(path))
                {
                    if (EnsureDirectoryExists(path))
                        return path;
                    // Can't create temporary files subdir!!!
                    return null;
                }
            }
        }
        #endregion

        #region EnsureDirectoryExists
        /// <summary>
        /// Ensure a directory exists (create directory structure if missing).
        /// <locDE><para />Stellt sicher, dass das Verzeichnis existiert (erstellt die Verzeichnisstruktur falls nötig).</locDE>
        /// </summary>
        /// <param name="path">The path/directory to check.<locDE><para />Zu prüfendes Verzeichnis.</locDE></param>
        /// <param name="stripFilename">Path is absolute filename, so the trailing filename has to be ignored?
        /// <locDE><para />Absoluter Dateiname angegeben, daher muss der Dateiname ignoriert werden?</locDE></param>
        /// <returns>True if path already existed or was created successfully.
        /// <locDE><para />True falls Verzeichnis bereits vorhanden oder erfolgreich erstellt.</locDE>
        /// </returns>
        public static bool EnsureDirectoryExists(string path, bool stripFilename = false)
        {
            try
            {
                if (stripFilename)
                    path = System.IO.Path.GetDirectoryName(path);

                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                return true;
            }
            catch { }
            return false;
        }
        #endregion

        #region CheckDirectoryWriteAccess
        /// <summary>
        /// Checks the directory write access.
        /// <locDE><para />Prüft Schreibzugriff auf ein Verzeichnis.</locDE>
        /// </summary>
        /// <param name="pathToCheck">The path to check.<locDE><para />Zu prüfendes Verzeichnis.</locDE></param>
        /// <param name="ensurePathExists">Should the directory structure be created if not existing?
        /// <locDE><para />Soll die Verzeichnisstruktur erzeugt werden falls noch nicht vorhanden?</locDE></param>
        /// <returns>True if write access is available, otherwise false.
        /// <locDE><para />True, falls Schreibzugriff möglich, sonst false.</locDE>
        /// </returns>
        public static bool CheckDirectoryWriteAccess(string pathToCheck, bool ensurePathExists = false)
        {
            try
            {
                if (ensurePathExists)
                {
                    if (!EnsureDirectoryExists(pathToCheck))
                        return false;
                }

                if (!System.IO.Directory.Exists(pathToCheck))
                    return false;

                string tempFile = GetTempFilename(pathToCheck, false);
                using (System.IO.TextWriter writer = new System.IO.StreamWriter(tempFile))
                {
                    writer.WriteLine("test");
                }
                // If no exception so far, test file was created successfully.
                // Try to delete test file:
                System.IO.File.Delete(tempFile);
                // Test file deleted successfully, full write access!
                return true;
            }
            catch { }
            return false;
        }
        #endregion

        #region CopyDirectoryFolder
        /// <summary>
        /// Copy directory folder data holder class.
        /// <locDE><para />CopyDirectoryFolder Datenhaltungsklasse.</locDE>
        /// </summary>
        private class CopyDirectoryFolder
        {
            /// <summary>
            /// Gets or sets the source.
            /// <locDE><para />Holt oder setzt die Quelle.</locDE>
            /// </summary>
            /// <value>The source.<locDE><para />Die Quelle.</locDE></value>
            public string Source { get; private set; }

            /// <summary>
            /// Gets or sets the target.
            /// <locDE><para />Holt oder setzt das Ziel.</locDE>
            /// </summary>
            /// <value>The target.<locDE><para />Das Ziel.</locDE></value>
            public string Target { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="CopyDirectoryFolder" /> class.
            /// <locDE><para />Initialisiert eine neue Instanz der <see cref="CopyDirectoryFolder" /> Klasse.</locDE>
            /// </summary>
            /// <param name="source">The source.<locDE><para />Die Quelle.</locDE></param>
            /// <param name="target">The target.<locDE><para />Das Ziel.</locDE></param>
            public CopyDirectoryFolder(string source, string target)
            {
                Source = source;
                Target = target;
            }
        }
        #endregion

        #region CopyDirectory
        /// <summary>
        /// Copies the directory with contents.
        /// <locDE><para />Kopiert das Verzeichnis samt Inhalt.</locDE>
        /// </summary>
        /// <param name="source">The source directory.<locDE><para />Das Quellverzeichnis.</locDE></param>
        /// <param name="target">The target directory.<locDE><para />Das Zielverzeichnis.</locDE></param>
        /// <param name="filePattern">The file pattern, i.e. "*.*".<locDE><para />Die Dateimaske, z.B. "*.*".</locDE></param>
        public static void CopyDirectory(string source, string target, string filePattern = "*.*")
        {
            var stack = new System.Collections.Generic.Stack<CopyDirectoryFolder>();
            stack.Push(new CopyDirectoryFolder(source, target));

            while (stack.Count > 0)
            {
                var folders = stack.Pop();
                System.IO.Directory.CreateDirectory(folders.Target);
                foreach (var file in System.IO.Directory.GetFiles(folders.Source, filePattern))
                {
                    System.IO.File.Copy(file, System.IO.Path.Combine(folders.Target, System.IO.Path.GetFileName(file)));
                }

                foreach (var folder in System.IO.Directory.GetDirectories(folders.Source))
                {
                    stack.Push(new CopyDirectoryFolder(folder, System.IO.Path.Combine(folders.Target, System.IO.Path.GetFileName(folder))));
                }
            }
        }
        #endregion

        #region IsLocalPath
        /// <summary>
        /// Determines whether the given path is on a local drive (no network drive).
        /// <locDE><para />Ermittelt, ob der angegebene Pfad auf einem lokalen Laufwerk liegt (kein Netzlaufwerk).</locDE>
        /// </summary>
        /// <param name="path">The path.<locDE><para />Der Pfad.</locDE></param>
        /// <returns>True, if the path is on a local drive.
        /// <locDE><para />True, falls der Pfad auf einen lokalen Laufwerk liegt.</locDE>
        /// </returns>
        public static bool IsLocalPath(string path)
        {
            System.IO.DriveType driveType = GetPathDriveType(path);
            switch (driveType)
            {
                case System.IO.DriveType.CDRom:
                case System.IO.DriveType.Fixed:
                case System.IO.DriveType.Ram:
                case System.IO.DriveType.Removable:
                    return true;
                    //case System.IO.DriveType.Network:
                    //case System.IO.DriveType.NoRootDirectory:
                    //case System.IO.DriveType.Unknown:
                    //    break;
            }
            return false;
        }
        #endregion

        #region GetPathDriveType
        /// <summary>
        /// Gets the drive type of the given path.
        /// <locDE><para />Ermittelt die Laufwerksart des angegebenen Pfades.</locDE>
        /// </summary>
        /// <param name="path">The path.<locDE><para />Der Pfad.</locDE></param>
        /// <returns>DriveType of path.<locDE><para />DriveType des Pfades.</locDE></returns>
        public static System.IO.DriveType GetPathDriveType(string path)
        {
            // http://stackoverflow.com/questions/4396634/how-can-i-determine-if-a-given-drive-letter-is-a-local-mapped-usb-drive

            if (string.IsNullOrWhiteSpace(path))
                return System.IO.DriveType.Unknown;

            // OK, so UNC paths aren't 'drives', but this is still handy
            if (path.StartsWith(@"\\"))
                return System.IO.DriveType.Network;

            System.IO.DriveInfo info = null;
            try
            {
                info = System.IO.DriveInfo.GetDrives().FirstOrDefault(x => path.StartsWith(x.Name, StringComparison.OrdinalIgnoreCase));
            }
            catch { }

            if (info == null)
                return System.IO.DriveType.Unknown;
            return info.DriveType;
        }
        #endregion

        #region GetTempFilename
        /// <summary>
        /// Gets an available (non-existing) temporary filename.
        /// <locDE><para />Ermittelt einen verfügbaren (nicht bereits vergebenen) temporären Dateinamen.</locDE>
        /// </summary>
        /// <param name="path">The path to create the file in. Null means default temporary directory.
        /// <locDE><para />Der Pfad, in dem die Datei erstellt werden soll. Null bedeutet im Standard-Temporärdateienverzeichnis.</locDE></param>
        /// <param name="mustBeLocal">Is the path required to be on a local drive (no network drive)?
        /// <locDE><para />Muss das Verzeichnis auf einem lokalen Laufwerk sein (kein Netzlaufwerk)?</locDE></param>
        /// <returns>Available (non-existing) temporary filename.
        /// <locDE><para />Verfügbarer (nicht bereits vergebener) temporärer Dateiname.</locDE>
        /// </returns>
        public static string GetTempFilename(string path = null, bool mustBeLocal = false)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = GetTempFilesPath(mustBeLocal);

            if (string.IsNullOrWhiteSpace(path))
                return null;

            while (true)
            {
                string filename = System.IO.Path.Combine(path, Guid.NewGuid().ToString().Trim('{', '}').Replace("-", ""));
                if (!System.IO.File.Exists(filename))
                    return filename;
            }
        }
        #endregion

        #region EnsureValidFilename
        /// <summary>
        /// Ensures a valid filename by replacing invalid characters by '_' (or own setting in <paramref name="replaceWith"/>).
        /// <locDE><para />Stellt einen gültigen Dateiname sicher, ersetzt ungültige Zeichen durch '_' (oder selbstgewähltes Zeichen in <paramref name="replaceWith"/>).</locDE>
        /// </summary>
        /// <param name="filename">The potentially invalid filename.<locDE><para />Der möglicherweise ungültige Dateiname.</locDE></param>
        /// <param name="pathContained">Is a relative or absolute path contained? If false, i.e. (back)slashes may not occur and will be replaced.
        /// <locDE><para />Ist ein relativer oder absoluter Pfad enthalten? Bei false sind bspw. Schrägstriche nicht erlaubt und werden ersetzt.</locDE></param>
        /// <param name="replaceWith">The replacement character(s) for invalid characters. Null means '_'. May be empty string (remove without replacement).
        /// <locDE><para />Die Ersetzungszeichen für ungültige Zeichen. Bei Null wird '_' verwendet. Bei leer werden ungültige Zeichen ersatzlos entfernt.</locDE></param>
        /// <returns>A valid filename.<locDE><para />Ein gültiger Dateiname.</locDE></returns>
        public static string EnsureValidFilename(string filename, bool pathContained = true, string replaceWith = null)
        {
            if (null == replaceWith)
                replaceWith = "_";

            string searchItem = null;

            foreach (char ch in System.IO.Path.GetInvalidFileNameChars())
            {
                searchItem = new string(new char[] { ch });
                filename = filename.Replace(searchItem, replaceWith);
            }

            if (pathContained)
            {
                // Filename and path combination
                foreach (char ch in System.IO.Path.GetInvalidPathChars())
                {
                    searchItem = new string(new char[] { ch });
                    filename = filename.Replace(searchItem, replaceWith);
                }
            }
            else
            {
                // Filename only, may not contain (back)slashes
                foreach (char ch in new char[] { '/', '\\' })
                {
                    searchItem = new string(new char[] { ch });
                    filename = filename.Replace(searchItem, replaceWith);
                }
            }
            return filename;
        }
        #endregion

        #region AnyFileExists
        /// <summary>
        /// Checks if at least one file matching the mask exists.
        /// <locDE><para />Prüft, ob zumindest eine zur Dateimaske passende Datei existiert.</locDE>
        /// </summary>
        /// <param name="path">The path.<locDE><para />Der Pfad.</locDE></param>
        /// <param name="fileMask">The file mask, i.e. "*.txt".<locDE><para />Die Dateimaske, z.B. "*.txt".</locDE></param>
        /// <returns><locDE><para />True, falls zumindest eine zur Dateimaske passende Datei existiert.</locDE></returns>
        public static bool AnyFileExists(string path, string fileMask)
        {
            try
            {
                return System.IO.Directory.EnumerateFiles(path, fileMask, System.IO.SearchOption.TopDirectoryOnly).Any();
            }
            catch { }
            return false;
        }
        #endregion

        #region FilenameFitsAnyMask
        /// <summary>
        /// Checks if given file name fits any of the given masks, i.e. "*.jpg|*.bmp".
        /// <locDE><para />Prüft, ob der angegebene Dateiname zu mind. einer der angegebenen Dateimasken passt, z.B. "*.jpg|*.bmp".</locDE>
        /// </summary>
        /// <param name="fileName">The file name.<locDE><para />Der Dateiname.</locDE></param>
        /// <param name="fileMasks">The file masks, i.e. "*.jpg|*.bmp".<locDE><para />Die Dateimasken, z.B. "*.jpg|*.bmp".</locDE></param>
        /// <param name="windowsStyle">Evaluate pattern in Windows style (true) or Unix style (false)?<locDE><para />Auswertung nach Windows-Art (true) oder Unix-Art (false)?</locDE></param>
        /// <returns>
        /// True, if the file name fits any of the given masks.
        /// <locDE><para />True, falls der Dateiname mind. einem der angegebenen Dateimasken entspricht.</locDE>
        /// </returns>
        public static bool FilenameFitsAnyMask(string fileName, string fileMasks, bool windowsStyle = true)
        {
            if (string.IsNullOrWhiteSpace(fileMasks))
                return false;

            return fileName.MatchesWildcard(fileMasks, windowsStyle);
        }
        #endregion
        #endregion
    }
}
