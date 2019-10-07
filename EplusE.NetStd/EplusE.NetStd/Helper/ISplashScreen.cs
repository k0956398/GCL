using System;

namespace EplusE
{
    /// <summary>
    /// Contract between splash screen and application to communicate.
    /// <locDE><para />Vertrag zur Kommunikation zwischen Splashscreen und Anwendung.</locDE>
    /// </summary>
    public interface ISplashScreen : IDisposable
    {
        #region Message

        /// <summary>
        /// The text message being displayed in the splash screen.
        /// <locDE><para />Der Text zur Anzeige im Spashscreen.</locDE>
        /// </summary>
        string Message { get; set; }

        #endregion Message

        #region PercentDone

        /// <summary>
        /// Gets or sets the percent done for progress display.
        /// <locDE><para />Holt/setzt die "Prozent erledigt" zur Anzeige im Fortschrittsbalken.</locDE>
        /// </summary>
        /// <value>
        /// The percent done.<locDE><para />Die "Prozent erledigt".</locDE>
        /// </value>
        int PercentDone { get; set; }

        #endregion PercentDone

        #region IsWindowAttached

        /// <summary>
        /// Has this instance a window attached?
        /// <locDE><para />Hat diese Instanz ein zugeordnetes Fenster?</locDE>
        /// </summary>
        /// <value>True if this instance has a window attached; otherwise false.
        /// <locDE><para />True, falls diese Instanz ein zugeordnetes Fenster hat, sonst false.</locDE>
        /// </value>
        bool IsWindowAttached { get; }

        #endregion IsWindowAttached

        #region Window

        /// <summary>
        /// Gets the splash window (Type should be System.Windows.Window).
        /// <locDE><para />Holt das Splash Fenster (Typ sollte System.Windows.Window sein).</locDE>
        /// </summary>
        /// <value>The splash window (Type should be System.Windows.Window).
        /// <locDE><para />Das Splash Fenster (Typ sollte System.Windows.Window sein).</locDE>
        /// </value>
        object Window { get; }

        #endregion Window

        #region SetAssemblyInfo

        /// <summary>
        /// Sets the assembly object.
        /// <locDE><para />Setzt das Assembly Objekt.</locDE>
        /// </summary>
        /// <remarks>
        /// Analyzes and formats the assembly information (Version, etc.) to display on the splash screen.
        /// <locDE><para />Analysiert und formatiert die Assemblyinformationen (Version, etc.) zur Anzeige am Splashscreen.</locDE>
        /// </remarks>
        /// <param name="assembly">The assembly to use.<locDE><para />Die zu verwendende Assembly.</locDE></param>
        void SetAssemblyInfo(System.Reflection.Assembly assembly);

        #endregion SetAssemblyInfo

        #region ShowMessage

        /// <summary>
        /// Shows a message at the splash screen.
        /// <locDE><para />Zeigt einen Text am Splashscreen.</locDE>
        /// </summary>
        /// <param name="message">The message to display.<locDE><para />Die anzuzeigende Nachricht.</locDE></param>
        /// <param name="percentDone">The percent done (0..100).
        /// Special values: -1 shows the progress indicator in indeterminate mode; -2 (default) doesn't affect the progress indicator.
        /// <locDE><para />Die "Prozent erledigt" (0..100).
        /// Sonderwerte: -1 zeigt den Fortschrittsbalken im "unbestimmten/ewigen" Modus; -2 (Standard) verändert den Fortschrittsbalken nicht.</locDE></param>
        void ShowMessage(string message, int percentDone = -2);

        #endregion ShowMessage

        #region ClearMessage

        /// <summary>
        /// Clears the message.
        /// <locDE><para />Löscht den Text.</locDE>
        /// </summary>
        void ClearMessage();

        #endregion ClearMessage

        #region Close

        /// <summary>
        /// Closes the window after the given time in milliseconds.
        /// <locDE><para />Schließt das Fenster nach der angegebenen Zeit in Millisekunden.</locDE>
        /// </summary>
        /// <param name="milliseconds">The wait time in milliseconds.<locDE><para />Wartezeit in Millisekunden.</locDE></param>
        void Close(int milliseconds = 0);

        #endregion Close

        #region Hide

        /// <summary>
        /// Hides the window after the given time in milliseconds.
        /// <locDE><para />Versteckt das Fenster nach der angegebenen Zeit in Millisekunden.</locDE>
        /// </summary>
        /// <param name="milliseconds">The wait time in milliseconds.<locDE><para />Wartezeit in Millisekunden.</locDE></param>
        void Hide(int milliseconds = 0);

        #endregion Hide

        #region Unhide

        /// <summary>
        /// Unhides the window after the given time in milliseconds.
        /// <locDE><para />Zeigt das Fenster nach der angegebenen Zeit in Millisekunden.</locDE>
        /// </summary>
        /// <param name="milliseconds">The wait time in milliseconds.<locDE><para />Wartezeit in Millisekunden.</locDE></param>
        void Unhide(int milliseconds = 0);

        #endregion Unhide
    }
}