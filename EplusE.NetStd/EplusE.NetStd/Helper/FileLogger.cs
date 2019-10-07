using EplusE.Log;
using System;
using System.IO;

namespace EplusE
{
    /// <summary>
    /// Simple logger class which writes into text file.
    /// <locDE><para />Einfache Logger-Klasse, die in eine Textdatei schreibt.</locDE>
    /// </summary>
    /// <remark>
    /// Don't move to namespace EplusE.Log (backward compatibility)!
    /// <locDE><para />Nicht in den Namespace EplusE.Log verschieben (Kompatibilität zu bestehenden Anwendungen)!</locDE>
    /// </remark>
    [Obsolete("Use EplusE.Log.AppLogger, see WpfApp1 for example.   Verwende EplusE.Log.AppLogger, siehe WpfApp1 Beispielprojekt.")]
    public static class FileLogger
    {
        // Alternative method for Trace (TraceSource, TraceEvent): 
        // Turning tracing off via app.config 
        // https://stackoverflow.com/questions/4144394/turning-tracing-off-via-app-config

        #region EntryLogged event
        /// <summary>
        /// Handler for EntryLogged event.
        /// <locDE><para />Ereignismethode für EntryLogged.</locDE>
        /// </summary>
        public delegate void EntryLoggedHandler(FileLoggerEntry entry);
        /// <summary>
        /// Occurs when log entry is written ("logged").
        /// <locDE><para />Tritt auf, wenn ein Logeintrag geschrieben wird.</locDE>
        /// </summary>
        public static event EntryLoggedHandler EntryLogged;

        /// <summary>
        /// Fire event EntryLogged.
        /// <locDE><para />Ereignis EntryLogged auslösen.</locDE>
        /// </summary>
        /// <param name="entry">The log entry.<locDE><para />Der Logeintrag.</locDE></param>
        internal static void OnEntryLogged(FileLoggerEntry entry)
        {
            var threadSafeEvent = EntryLogged;
            if (null != threadSafeEvent)
                threadSafeEvent(entry);
        }

        /// <summary>
        /// Are there any subscribers for EntryLogged event?
        /// <locDE><para />Gibt es Abonnenten für das Ereignis EntryLogged?</locDE>
        /// </summary>
        internal static bool HasEntryLoggedSubscribers
        {
            get
            {
                return null != EntryLogged;
            }
        }
        #endregion

        #region CallerNameLayout enum
        /// <summary>
        /// Caller name layout enumeration.
        /// <locDE><para />Aufzählung der Aufrufername-Darstellungsvarianten.</locDE>
        /// </summary>
        public enum CallerNameLayout
        {
            /// <summary>
            /// No caller name at all.
            /// <locDE><para />Keinen Aufrufernamen darstellen.</locDE>
            /// </summary>
            NoCallerName = 0,

            /// <summary>
            /// Method name only (without namespace and class name).
            /// <locDE><para />Nur Methodennamen (ohne Namespace und Klassenbezeichnung).</locDE>
            /// </summary>
            MethodNameOnly = 1,

            /// <summary>
            /// Class and method name (without namespace).
            /// <locDE><para />Klassen- und Methodennamen (ohne Namespace).</locDE>
            /// </summary>
            ClassAndMethodName = 2,

            /// <summary>
            /// Namespace, class and method name.
            /// <locDE><para />Namespace, Klassen- und Methodennamen.</locDE>
            /// </summary>
            FullMethodName = 3
        }
        #endregion

        #region Data fields
        /// <summary>
        /// Log message timestamp format, i.e. "yyyy-MM-dd HH:mm:ss".
        /// <locDE><para />Logeintrag Zeitstempel Format, z.B. "yyyy-MM-dd HH:mm:ss".</locDE>
        /// </summary>
        public static string LogMsgTimestampFormat = AppLogger.Helper.LogMsgTimestampFormat;
        /// <summary>
        /// Log message timestamp prefix, i.e. "[".
        /// <locDE><para />Logeintrag Zeitstempel Präfix, z.B. "[".</locDE>
        /// </summary>
        public static string LogMsgTimestampPrefix = AppLogger.Helper.LogMsgTimestampPrefix;
        /// <summary>
        /// Log message timestamp suffix, i.e. "] ".
        /// <locDE><para />Logeintrag Zeitstempel Suffix, z.B. "] ".</locDE>
        /// </summary>
        public static string LogMsgTimestampSuffix = AppLogger.Helper.LogMsgTimestampSuffix;
        /// <summary>
        /// Log message caller prefix, i.e. "- ".
        /// <locDE><para />Logeintrag Aufrufer Präfix, z.B. "- ".</locDE>
        /// </summary>
        public static string LogMsgCallerPrefix = AppLogger.Helper.LogMsgCallerPrefix;
        /// <summary>
        /// Log message caller suffix, i.e. ": ".
        /// <locDE><para />Logeintrag Aufrufer Suffix, z.B. ": ".</locDE>
        /// </summary>
        public static string LogMsgCallerSuffix = AppLogger.Helper.LogMsgCallerSuffix;
        /// <summary>
        /// Log message text prefix, i.e. "".
        /// <locDE><para />Logeintrag Text Präfix, z.B. "".</locDE>
        /// </summary>
        public static string LogMsgTextPrefix = AppLogger.Helper.LogMsgTextPrefix;
        /// <summary>
        /// Log message text suffix, i.e. "".
        /// <locDE><para />Logeintrag Text Suffix, z.B. "".</locDE>
        /// </summary>
        public static string LogMsgTextSuffix = AppLogger.Helper.LogMsgTextSuffix;
        /// <summary>
        /// Log message caller name layout (if caller name is not given and detected automatically), i.e. MethodNameOnly.
        /// <locDE><para />Logeintrag Aufrufername-Darstellungsvariante (bei Auto-Erkennung), z.B. MethodNameOnly.</locDE>
        /// </summary>
        public static CallerNameLayout LogMsgCallerNameLayout = (CallerNameLayout)((int)AppLogger.Helper.LogMsgCallerNameLayout);

        /// <summary>
        /// Log performance timestamp format, i.e. "HH:mm:ss.fff".
        /// <locDE><para />Performanz-Logging Zeitstempel Format, z.B. "HH:mm:ss.fff".</locDE>
        /// </summary>
        public static string LogPerformanceTimestampFormat = AppLogger.Helper.LogPerformanceTimestampFormat;
        /// <summary>
        /// Log performance timestamp prefix, i.e. "[".
        /// <locDE><para />Performanz-Logging Zeitstempel Präfix, z.B. "[".</locDE>
        /// </summary>
        public static string LogPerformanceTimestampPrefix = AppLogger.Helper.LogPerformanceTimestampPrefix;
        /// <summary>
        /// Log performance timestamp suffix, i.e. "] ".
        /// <locDE><para />Performanz-Logging Zeitstempel Suffix, z.B. "] ".</locDE>
        /// </summary>
        public static string LogPerformanceTimestampSuffix = AppLogger.Helper.LogPerformanceTimestampSuffix;

        /// <summary>
        /// New line character(s) to be used for logging, i.e. "\r\n".
        /// <locDE><para />Zeilenumbruch-Zeichen für Logeinträge, z.B. "\r\n".</locDE>
        /// </summary>
        public static string LogNewLine = AppLogger.Helper.LogNewLine;

        /// <summary>
        /// Log file name, i.e. "LogFile.txt". 
        /// Set to null if no log file should be written/created.
        /// </summary>
        public static string LogFileName = "LogFile.txt";

        /// <summary>
        /// Log base directory, typically application root from AppHelper.GetAppPath().
        /// </summary>
        public static string LogBaseDir = null;

        /// <summary>
        /// Log level: 0 = no outputs, 9 = verbose outputs.
        /// <locDE><para />LogLevel: 0 = unterdrücken, 9 = detailliert.</locDE>
        /// </summary>
        public static int LogLevel = AppLogger.Helper.LogLevel;

        /// <summary>
        /// Should System.Diagnostics.Debug.WriteLine diagnostic outputs be generated?
        /// <locDE><para />Sollen System.Diagnostics.Debug.WriteLine Ausgaben erzeugt werden?</locDE>
        /// </summary>
        public static bool TraceOutputsEnabled = AppLogger.Helper.TraceOutputsEnabled;

        /// <summary>
        /// Should Console.WriteLine diagnostic outputs be generated?
        /// <locDE><para />Sollen Console.WriteLine Ausgaben erzeugt werden?</locDE>
        /// </summary>
        public static bool ConsoleOutputsEnabled = AppLogger.Helper.ConsoleOutputsEnabled;

        /// <summary>
        /// Performance log enabled by configuration?
        /// <locDE><para />Performanz-Logging in Konfiguration aktiviert?</locDE>
        /// </summary>
        public static bool LogPerformanceEnabled = AppLogger.Helper.LogPerformanceEnabledByConfig;

        /// <summary>
        /// Locker object to synchronize log file access.
        /// <locDE><para />Locker Objekt für die Synchronisation des Logdatei-Zugriffs.</locDE>
        /// </summary>
        private static object _logFileLocker = new object();
        #endregion

        #region Constructor
        /// <summary>
        /// Static constructor.
        /// <locDE><para />Statischer Konstruktor.</locDE>
        /// </summary>
        static FileLogger()
        {
        }
        #endregion

        #region WillBeLogged
        /// <summary>
        /// Checks if a log message of given severity will be logged or ignored.
        /// <locDE><para />Prüft, ob ein Logeintrag mit dem angegebenen Schweregrad geloggt oder ignoriert wird.</locDE>
        /// </summary>
        /// <param name="severity">The severity.<locDE><para />Der Schweregrad.</locDE></param>
        /// <returns>True if log message will be logged; false if it will be ignored.
        /// <locDE><para />True falls Logeintrag geloggt werden würde; false falls dieser ignoriert werden würde.</locDE></returns>
        public static bool WillBeLogged(LogSeverity severity = LogSeverity.Info)
        {
            return WillBeLogged((int)severity);
        }

        /// <summary>
        /// Checks if a log message of given severity will be logged or ignored.
        /// <locDE><para />Prüft, ob ein Logeintrag mit dem angegebenen Schweregrad geloggt oder ignoriert wird.</locDE>
        /// </summary>
        /// <param name="logLevel">The log level (0 = no outputs, 9 = verbose outputs).
        /// <locDE><para />Der Log Level (0 = unterdrücken, 9 = detailliert).</locDE></param>
        /// <returns>True if log message will be logged; false if it will be ignored.
        /// <locDE><para />True falls Logeintrag geloggt werden würde; false falls dieser ignoriert werden würde.</locDE></returns>
        public static bool WillBeLogged(int logLevel = AppLogger.Helper.DefaultEntryLogLevel)
        {
            // Check if severity (log level) is important enough to pass through
            // Prüfe, ob Schweregrad (Log Level) groß/wichtig genug ist, um weiterbearbeitet zu werden
            if (logLevel > AppLogger.Helper.LogLevel)
                return false;

            return true;
        }
        #endregion

        #region Log
        /// <summary>
        /// Logs the specified text (or an empty line/entry).
        /// <locDE><para />Loggt den angegebenen Text (oder eine Leerzeile/einen Leereintrag).</locDE>
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Der zu loggende Text. Falls null oder leer wird eine Leerzeile/ein Leereintrag geloggt.</locDE></param>
        /// <param name="severity">The severity (log level).<locDE><para />Der Schweregrad (Log Level).</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void Log(string text, LogSeverity severity, string caller = null)
        {
            caller = AppLogger.Helper.ResolveCaller(caller);

            Log_Internal(text, (int)severity, caller, (Exception)null);
        }

        /// <summary>
        /// Logs the specified text (or an empty line/entry).
        /// <locDE><para />Loggt den angegebenen Text (oder eine Leerzeile/einen Leereintrag).</locDE>
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Der zu loggende Text. Falls null oder leer wird eine Leerzeile/ein Leereintrag geloggt.</locDE></param>
        /// <param name="logLevel">The log level (0 = no outputs, 9 = verbose outputs).
        /// <locDE><para />Der LogLevel (0 = unterdrücken, 9 = detailliert).</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void Log(string text, int logLevel = AppLogger.Helper.DefaultEntryLogLevel, string caller = null)
        {
            caller = AppLogger.Helper.ResolveCaller(caller);

            Log_Internal(text, logLevel, caller, (Exception)null);
        }

        /// <summary>
        /// Logs the specified exception.
        /// <locDE><para />Loggt die angegebene Exception.</locDE>
        /// </summary>
        /// <param name="ex">The exception.<locDE><para />Die Exception.</locDE></param>
        /// <param name="additionalInformation">The additional information.<locDE><para />Die Zusatzinformation.</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void Log(Exception ex, string additionalInformation, string caller = null)
        {
            if (null == ex)
                return;

            caller = AppLogger.Helper.ResolveCaller(caller);

            Log(ex, AppLogger.Helper.DefaultEntryLogLevel, additionalInformation, caller);
        }

        /// <summary>
        /// Logs the specified exception.
        /// <locDE><para />Loggt die angegebene Exception.</locDE>
        /// </summary>
        /// <param name="ex">The exception.<locDE><para />Die Exception.</locDE></param>
        /// <param name="severity">The severity (log level).<locDE><para />Der Schweregrad (Log Level).</locDE></param>
        /// <param name="additionalInformation">The additional information.<locDE><para />Die Zusatzinformation.</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void Log(Exception ex, LogSeverity severity, string additionalInformation = null, string caller = null)
        {
            if (null == ex)
                return;

            caller = AppLogger.Helper.ResolveCaller(caller);

            Log(ex, (int)severity, additionalInformation, caller);
        }

        /// <summary>
        /// Logs the specified exception.
        /// <locDE><para />Loggt die angegebene Exception.</locDE>
        /// </summary>
        /// <param name="ex">The exception.<locDE><para />Die Exception.</locDE></param>
        /// <param name="logLevel">The log level (0 = no outputs, 9 = verbose outputs).
        /// <locDE><para />Der LogLevel (0 = unterdrücken, 9 = detailliert).</locDE></param>
        /// <param name="additionalInformation">The additional information.<locDE><para />Die Zusatzinformation.</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void Log(Exception ex, int logLevel = AppLogger.Helper.DefaultEntryLogLevel, string additionalInformation = null, string caller = null)
        {
            if (null == ex)
                return;

            caller = AppLogger.Helper.ResolveCaller(caller);

            string text = CreateExceptionMessage(ex, additionalInformation);

            Log_Internal(text, logLevel, caller, ex);
        }

        /// <summary>
        /// Logs the specified text (or an empty line/entry).
        /// <locDE><para />Loggt den angegebenen Text (oder eine Leerzeile/einen Leereintrag).</locDE>
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Der zu loggende Text. Falls null oder leer wird eine Leerzeile/ein Leereintrag geloggt.</locDE></param>
        /// <param name="logLevel">The log level (0 = no outputs, 9 = verbose outputs).
        /// <locDE><para />Der LogLevel (0 = unterdrücken, 9 = detailliert).</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        /// <param name="ex">The exception object (if any, otherwise null).
        /// <locDE><para />Das Exception-Objekt (falls vorhanden, sonst null).</locDE></param>
        private static void Log_Internal(string text, int logLevel, string caller, Exception ex)
        {
            // Check if severity (log level) is important enough to pass through
            // Prüfe, ob Schweregrad (Log Level) groß/wichtig genug ist, um weiterbearbeitet zu werden
            if (logLevel > LogLevel)
                return;

            // Would be too late: Caller must be resolved by public method, as otherwise the used stack trace index does not match!
            // Wäre zu spät: Aufrufer muss durch die öffentliche Methode aufgelöst werden, sonst passt der Stack Trace Index nicht mehr!
            //caller = ResolveCaller(caller);

            try
            {
                #region Add decorations, fire FileLogger specific event
                string decoratedText = AddTextDecorations(text, (LogSeverity)logLevel, caller);

                #region Notify registered event callbacks (if any) about new log entry
                if (null != EntryLogged)
                {
                    FileLoggerEntry entry = new FileLoggerEntry();
                    entry.LogLevel = logLevel;
                    entry.Caller = caller;
                    entry.Timestamp = DateTime.Now;
                    entry.Text = text;
                    entry.LogMessage = decoratedText;
                    if (null != ex)
                        entry.Exception = ex;

                    OnEntryLogged(entry);
                }
                #endregion

                text = decoratedText;
                #endregion

                #region Trace and console outputs
                if (TraceOutputsEnabled)
                    System.Diagnostics.Debug.WriteLine(text ?? "");

                if (ConsoleOutputsEnabled)
                    Console.WriteLine(text ?? "");
                #endregion

                WriteLogEntry(text);
            }
            catch { }
        }
        #endregion

        #region LogPerformance
        /// <summary>
        /// Logs the application performance. Message typically contains milliseconds since previous performance log entry.
        /// <locDE><para />Loggt die Anwendungs-Performanz. Eintrag enthält typischerweise die Millisekunden seit dem vorhergehenden Performanz-Logeintrag.</locDE>
        /// </summary>
        /// <param name="text">The text to log.<locDE><para />Der zu loggende Text.</locDE></param>
        /// <param name="caller">The caller (method), null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufer (Methodenname), null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        public static void LogPerformance(string text, string caller = null)
        {
            if (!LogPerformanceEnabled)
                return;

            caller = AppLogger.Helper.ResolveCaller(caller);

            try
            {
                #region Check if this is the first LogPerformance message
                if (AppLogger.Helper.IsFirstLogPerformanceMessage())
                {
                    // Create first entry with at least date (as the date is most probably not included in the further entries)
                    // Erzeuge einen Eintrag, der zumindest das Datum enthält (da die Folgeeinträge vermutlich nur Zeitinformation enthalten)
                    string initialLine = CreateLogPerformanceMessage("Created at " + DateTime.Now.ToString(LogMsgTimestampFormat ?? "yyyy-MM-dd"), "");

                    WriteLogEntry("");
                    WriteLogEntry(initialLine);
                }
                #endregion

                string strMsg = CreateLogPerformanceMessage(text, caller);

                #region Trace and console outputs
                if (TraceOutputsEnabled)
                    System.Diagnostics.Debug.WriteLine(strMsg);

                if (ConsoleOutputsEnabled)
                    Console.WriteLine(strMsg);
                #endregion

                WriteLogEntry(strMsg);
            }
            catch { }
        }
        #endregion

        #region WriteLogEntry
        /// <summary>
        /// Writes the given text into the log as it is.
        /// <locDE><para />Schreibt den angegebenen Text direkt (ohne jegliche Formatierungen/Ersetzungen) ins Log.</locDE>
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Der zu loggende Text. Falls null oder leer wird eine Leerzeile/ein Leereintrag geloggt.</locDE></param>
        /// <param name="severity">The severity (log level).<locDE><para />Der Schweregrad (Log Level).</locDE></param>
        internal static void WriteLogEntry(string text, LogSeverity severity = LogSeverity.Info)
        {
            try
            {
                string logFileName = GetLogFileName();
                if (null == logFileName)
                    return;

                lock (_logFileLocker)
                {
                    using (StreamWriter writer = new StreamWriter(logFileName, true))
                    {
                        writer.WriteLine(text ?? "");
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Helper methods
        #region GetLogFileName
        /// <summary>
        /// Get log file name, create the file if not yet existing.
        /// <locDE><para />Holt den Logdateinamen, erzeugt die Datei falls es diese noch nicht gibt.</locDE>
        ///</summary>
        /// <returns>Log file name (or null on any error).<locDE><para />Logdateiname (oder null im Fehlerfall).</locDE></returns>
        private static string GetLogFileName()
        {
            if (string.IsNullOrWhiteSpace(LogFileName))
                return null;

            try
            {
                // Get the base directory (if not set explicitly, use GetAppPath)
                if (null == LogBaseDir)
                    LogBaseDir = AppHelper.GetAppPath();

                // Build full qualified file name
                string logFileName = Path.Combine(LogBaseDir, LogFileName);

                // If exists, return the path
                if (File.Exists(logFileName))
                {
                    return logFileName;
                }
                else
                {
                    // Create a text file
                    if (!AppHelper.EnsureDirectoryExists(logFileName, true))
                        return null;

                    FileStream fs = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }
                return logFileName;
            }
            catch { }
            return null;
        }
        #endregion

        #region CreateExceptionMessage
        /// <summary>
        /// Converts an exception into a meaningful message.
        /// <locDE><para />Erzeugt sinnvollen Text aus einer Exception.</locDE>
        /// </summary>
        /// <param name="ex">The exception object.<locDE><para />Das Exception-Objekt.</locDE></param>
        /// <param name="additionalInformation">The additional information.<locDE><para />Die Zusatzinformation</locDE></param>
        /// <returns>Text describing exception.<locDE><para />Text, der die Exception beschreibt.</locDE></returns>
        private static string CreateExceptionMessage(Exception ex, string additionalInformation = null)
        {
            // NOTE: Duplicate implementation in AppLogger.cs

            if (null == ex)
                return null;

            System.Text.StringBuilder msg = new System.Text.StringBuilder();

            msg.Append(ex.GetType().ToString());
            msg.Append(": ");
            msg.Append(ex.Message ?? "[no Message]");

            #region List messages of inner exceptions
            Exception innerEx = ex.InnerException;
            while (null != innerEx)
            {
                msg.Append(LogNewLine);
                msg.Append(innerEx.GetType().ToString());
                msg.Append(": ");
                msg.Append(innerEx.Message);
                innerEx = innerEx.InnerException;
            }
            #endregion

            if (!string.IsNullOrWhiteSpace(additionalInformation))
            {
                msg.Append(LogNewLine);
                msg.Append("Additional information: ");
                msg.Append(additionalInformation);
            }

            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                msg.Append(LogNewLine);
                msg.Append("Stack Trace: ");
                msg.Append(ex.StackTrace);
            }
            return msg.ToString();
        }
        #endregion

        #region CreateLogPerformanceMessage
        /// <summary>
        /// Creates a log performance message.
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Loggt den angegebenen Text (oder eine Leerzeile/einen Leereintrag).</locDE></param>
        /// <param name="caller">The caller name. Null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufername. Null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        /// <returns>Text for performance log.<locDE><para />Text für das Performanz-Log.</locDE></returns>
        private static string CreateLogPerformanceMessage(string text, string caller = null)
        {
            // NOTE: Duplicate implementation in AppLogger.cs

            DateTime tsUtc = DateTime.UtcNow;
            // Timestamp format, i.e. "HH:mm:ss.fff"
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            msg.Append(DateTime.Now.ToString(LogPerformanceTimestampFormat ?? ""));
            msg.Append(LogPerformanceTimestampPrefix ?? "");
            msg.Append(string.Format("{0:00000}", tsUtc.Subtract(AppLogger.Helper._tsLastPerformanceLogEntry).TotalMilliseconds));
            msg.Append(LogPerformanceTimestampSuffix ?? "");

            if (!string.IsNullOrWhiteSpace(caller))
            {
                // typically: "- " + caller + ": "
                msg.Append(LogMsgCallerPrefix ?? "");
                msg.Append(caller);
                msg.Append(LogMsgCallerSuffix ?? "");
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                // typically no (empty) prefix/suffix
                msg.Append(LogMsgTextPrefix ?? "");
                msg.Append(text);
                msg.Append(LogMsgTextSuffix ?? "");
            }

            text = msg.ToString();

            AppLogger.Helper._tsLastPerformanceLogEntry = tsUtc;

            return text;
        }
        #endregion

        #region AddTextDecorations
        /// <summary>
        /// Adds the text decorations.
        /// <locDE><para />Fügt dem Text Präfix/Suffix usw. hinzu.</locDE>
        /// </summary>
        /// <param name="text">The text to log. If null or empty, just an empty line/entry is logged.
        /// <locDE><para />Loggt den angegebenen Text (oder eine Leerzeile/einen Leereintrag).</locDE></param>
        /// <param name="severity">The severity (log level).<locDE><para />Der Schweregrad (Log Level).</locDE></param>
        /// <param name="caller">The caller name. Null means auto detect, empty means no/unspecified caller.
        /// <locDE><para />Der Aufrufername. Null heißt Auto-Erkennung, leer heißt kein/unspezifizierter Aufrufer.</locDE></param>
        /// <returns>Decorated text.<locDE><para />Ausgeschmückter Text.</locDE></returns>
        private static string AddTextDecorations(string text, LogSeverity severity, string caller)
        {
            if (!string.IsNullOrEmpty(text))
            {
                System.Text.StringBuilder msg = new System.Text.StringBuilder();

                // Add a log entry row
                DateTime now = DateTime.Now;
                // Timestamp format, i.e. "yyyy-MM-dd HH:mm:ss"
                string timestamp = string.Format("{0:" + (LogMsgTimestampFormat ?? "") + "}", now);
                msg.Append(LogMsgTimestampPrefix ?? "");
                msg.Append(timestamp);
                msg.Append(LogMsgTimestampSuffix ?? "");

                if (!string.IsNullOrWhiteSpace(caller))
                {
                    // Typically: "- " + caller + ": "
                    msg.Append(LogMsgCallerPrefix ?? "");
                    msg.Append(caller);
                    msg.Append(LogMsgCallerSuffix ?? "");
                }

                if (!string.IsNullOrWhiteSpace(text))
                {
                    // Typically no (empty) prefix/suffix
                    msg.Append(LogMsgTextPrefix ?? "");
                    msg.Append(text);
                    msg.Append(LogMsgTextSuffix ?? "");
                }

                text = msg.ToString();
            }
            return text;
        }
        #endregion
        #endregion
    }
}