namespace EplusE
{
    /// <summary>
    /// Log severity (log level) enumeration.
    /// <locDE><para />Log Schweregrad Aufzählung.</locDE>
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Do not log (disabled).
        /// <locDE><para />Nicht loggen (deaktiviert).</locDE>
        /// </summary>
        Off = 0,

        /// <summary>
        /// Fatal error. Use this for very serious errors!
        /// <locDE><para />Fataler Fehler. Verwenden für schwerwiegende Fehler!</locDE>
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// Error. Use this for error messages - most of the time these are Exceptions.
        /// <locDE><para />Fehler. Verwenden für Fehler wie z.B. Exceptions verwenden.</locDE>
        /// </summary>
        Error = 2,

        /// <summary>
        /// Warning. Use this for warning messages, typically for non-critical issues, which can be recovered or which are temporary failures.
        /// <locDE><para />Warnung. Verwenden für Warnmeldungen, typischerweise unkritische Vorgänge, welche behebbar sind oder nur vorübergehend bestehen.</locDE>
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Information. Use this for information messages, which are normally enabled in production environment.
        /// <locDE><para />Information. Verwenden für informative Nachrichten, die üblicherweise auch in der Produktivumgebung aktiviert sind.</locDE>
        /// </summary>
        Info = 4,

        /// <summary>
        /// Debug message. Use this for debugging information, less detailed than trace, typically not enabled in production environment.
        /// <locDE><para />Debug-Information. Verwenden für Debugging Informationen, weniger detailliert als Trace, typischerweise nicht aktiviert in Produktivumgebungen.</locDE>
        /// </summary>
        Debug = 5,

        /// <summary>
        /// Trace message (Level 6).
        /// Use this for very detailed logs, which may include high-volume information such as protocol payloads.
        /// This log level is typically only enabled during development.
        /// <locDE><para />Trace-Information (Ebene 6). Verwenden für detaillierte Logs, kann größere Datenmengen beinhalten. Typischerweise nur während der Entwicklung aktiv.</locDE>
        /// </summary>
        Trace6 = 6,

        /// <summary>
        /// Trace message (Level 7).
        /// Use this for very detailed logs, which may include high-volume information such as protocol payloads.
        /// This log level is typically only enabled during development.
        /// <locDE><para />Trace-Information (Ebene 7). Verwenden für detaillierte Logs, kann größere Datenmengen beinhalten. Typischerweise nur während der Entwicklung aktiv.</locDE>
        /// </summary>
        Trace7 = 7,

        /// <summary>
        /// Trace message (Level 8).
        /// Use this for very detailed logs, which may include high-volume information such as protocol payloads.
        /// This log level is typically only enabled during development.
        /// <locDE><para />Trace-Information (Ebene 8). Verwenden für detaillierte Logs, kann größere Datenmengen beinhalten. Typischerweise nur während der Entwicklung aktiv.</locDE>
        /// </summary>
        Trace8 = 8,

        /// <summary>
        /// Trace message (Level 9).
        /// Use this for very detailed logs, which may include high-volume information such as protocol payloads.
        /// This log level is typically only enabled during development.
        /// <locDE><para />Trace-Information (Ebene 9). Verwenden für detaillierte Logs, kann größere Datenmengen beinhalten. Typischerweise nur während der Entwicklung aktiv.</locDE>
        /// </summary>
        Trace9 = 9
    }
}