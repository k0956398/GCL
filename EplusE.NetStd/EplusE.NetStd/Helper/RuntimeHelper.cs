namespace EplusE
{
    /// <summary>
    /// Provides methods for determining if running in Designer, etc.
    /// <locDE><para />Stellt Methoden bereit, die ermitteln, ob der Code vom Designer ausgeführt wird, usw.</locDE>
    /// </summary>
    public static class RuntimeHelper
    {
        #region Design time, running in IDE, etc. helpers

        /// <summary>
        /// Determines if code is executing in Designer (XAML preview).
        /// <locDE><para />Ermittelt, ob der Code im Designer (XAML Vorschau) ausgeführt wird.</locDE>
        /// </summary>
        public static bool IsDesignTime
        {
            get
            {
                if (System.ComponentModel.LicenseUsageMode.Designtime == System.ComponentModel.LicenseManager.UsageMode)
                    return true;

                using (var process = System.Diagnostics.Process.GetCurrentProcess())
                {
                    return process.ProcessName.ToLowerInvariant().Contains("devenv");
                }
            }
        }

        /// <summary>
        /// Determines if code is executing in Designer (XAML preview) or inside IDE (Visual Studio, Debugger).
        /// <locDE><para />Ermittelt, ob der Code im Designer (XAML Vorschau) oder in der IDE (Visual Studio, Debugger) ausgeführt wird.</locDE>
        /// </summary>
        public static bool IsDesignTimeOrInsideIDE
        {
            get
            {
                return IsDesignTime || IsInsideIDE;
            }
        }

        /// <summary>
        /// Determines if code is executing inside/from IDE (Visual Studio, Debugger).
        /// <locDE><para />Ermittelt, ob der Code in der IDE (Visual Studio, Debugger) ausgeführt wird.</locDE>
        /// </summary>
        public static bool IsInsideIDE
        {
            get
            {
                return System.Diagnostics.Debugger.IsAttached;

                //using (var process = System.Diagnostics.Process.GetCurrentProcess())
                //{
                //    return process.ProcessName.ToLowerInvariant().Contains("devenv");
                //}
            }
        }

        /// <summary>
        /// Determines if code is executing at runtime (NOT at design time/development).
        /// <locDE><para />Ermittelt, ob der Code zur Laufzeit (NICHT zur Designzeit/Entwicklung) ausgeführt wird.</locDE>
        /// </summary>
        public static bool IsRunTime
        {
            get
            {
                return !IsDesignTime;
            }
        }

        #endregion Design time, running in IDE, etc. helpers

        #region IsRunningOnASPnet

        /// <summary>
        /// Is the current app instance executed on ASP.net?
        /// <locDE><para />Wird die Anwendungsinstanz unter ASP.net ausgeführt?</locDE>
        /// </summary>
        /// <value>
        /// Is the current app instance executed on ASP.net?
        /// <locDE><para />Wird die Anwendungsinstanz unter ASP.net ausgeführt?</locDE>
        /// </value>
        public static bool IsRunningOnASPnet { get; internal set; }

        #endregion IsRunningOnASPnet
    }
}