using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace EplusE.Helper
{
    /// <summary>
    /// Opens a console window to receive Console.Write(Line) outputs, i.e. for a WPF application.
    /// <locDE><para />Öffnet ein Konsolenfenster für Console.Write(Line) Ausgaben, z.B. für eine WPF-Anwendung.</locDE>
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/160587/no-output-to-console-from-a-wpf-application
    /// <locDE><para />Siehe http://stackoverflow.com/questions/160587/no-output-to-console-from-a-wpf-application </locDE>
    /// </remarks>
    [SuppressUnmanagedCodeSecurity]
    public static class ConsoleManager
    {
        private const string Kernel32_DllName = "kernel32.dll";

        #region HasConsole

        /// <summary>
        /// Has this process a console window?
        /// <locDE><para />Hat dieser Prozess ein Konsolenfenster?</locDE>
        /// </summary>
        /// <value>True if this process has console; otherwise false.
        /// <locDE><para />True falls dieser Prozess ein Konsolenfenster hat, sonst false.</locDE>
        /// </value>
        public static bool HasConsole
        {
            get { return GetConsoleWindow() != IntPtr.Zero; }
        }

        #endregion HasConsole

        #region Hide

        /// <summary>
        /// If the process has a console attached to it, it will be detached and no longer visible.
        /// Writing to the System.Console is still possible, but no output will be shown.
        /// <locDE><para />Falls dieser Prozess ein Konsolenfenster hat, wird es abgekoppelt und versteckt.
        /// Aufrufe von System.Console sind weiterhin möglich, werden aber nicht angezeigt.</locDE>
        /// </summary>
        public static void Hide()
        {
            //#if DEBUG
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
            //#endif
        }

        #endregion Hide

        #region Show

        /// <summary>
        /// Creates a new console instance if the process is not attached to a console already.
        /// <locDE><para />Erzeugt ein Konsolenfenster für diesen Prozess (falls noch keines vorhanden).</locDE>
        /// </summary>
        public static void Show()
        {
            //#if DEBUG
            if (!HasConsole)
            {
                AllocConsole();
                InvalidateOutAndError();
            }
            //#endif
        }

        #endregion Show

        #region Toggle

        /// <summary>
        /// Toggles the visibility (existence) of the console.
        /// <locDE><para />Schaltet die Sichtbarkeit (Existenz) des Konsolenfensters um.</locDE>
        /// </summary>
        public static void Toggle()
        {
            if (HasConsole)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        #endregion Toggle

        #region Private methods

        [DllImport(Kernel32_DllName)]
        private static extern bool AllocConsole();

        [DllImport(Kernel32_DllName)]
        private static extern bool FreeConsole();

        [DllImport(Kernel32_DllName)]
        private static extern int GetConsoleOutputCP();

        [DllImport(Kernel32_DllName)]
        private static extern IntPtr GetConsoleWindow();

        private static void InvalidateOutAndError()
        {
            Type type = typeof(System.Console);

            System.Reflection.FieldInfo _out = type.GetField("_out",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.FieldInfo _error = type.GetField("_error",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod("InitializeStdOutError",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            Debug.Assert(_out != null);
            Debug.Assert(_error != null);

            Debug.Assert(_InitializeStdOutError != null);

            _out.SetValue(null, null);
            _error.SetValue(null, null);

            _InitializeStdOutError.Invoke(null, new object[] { true });
        }

        private static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }

        #endregion Private methods
    }
}