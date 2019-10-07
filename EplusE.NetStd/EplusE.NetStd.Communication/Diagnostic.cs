using System;

namespace EplusE.NetStd.Communication
{
    public static class Diagnostic
    {
        static private object _lock = new object();
        static private DateTime _tsLastDiagnosticMsg;
        static public int OutputLevel { get; set; } = 1; // 0 = no outputs, 9 = verbose outputs

        /// <summary>
        /// Outputs a diagnostic message; adds timestamp and time delta [msec] (time passed since
        /// last message).
        /// Note: Should only be called depending on DiagnosticOutputsLevel!
        /// </summary>
        /// <param name="level">Diagnostic level of message</param>
        /// <param name="caller">Caller name to identify the source of this diagnostic message.</param>
        /// <param name="msg">Diagnostic information WITHOUT timestamp (added automatically)</param>
        public static void Msg(int level, string caller, string msg)
        {
            if (OutputLevel < level)
                return;
            lock (_lock)
            {
                DateTime ts = DateTime.UtcNow;

                System.Diagnostics.Trace.TraceInformation(
                    ts.ToString("HH:mm:ss.fff") +
                    " [" + string.Format("{0:00000}", ts.Subtract(_tsLastDiagnosticMsg).TotalMilliseconds) + "] - " + caller + ": " + msg);

                _tsLastDiagnosticMsg = DateTime.UtcNow;
            }
        }
    }
}