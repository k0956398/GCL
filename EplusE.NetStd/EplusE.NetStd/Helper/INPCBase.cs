using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EplusE
{
    /// <summary>
    /// INotifyPropertyChanged implementation base class.
    /// <locDE><para />INotifyPropertyChanged Implementations-Basisklasse.</locDE>
    /// </summary>
    public class INPCBase : INotifyPropertyChanged
    {
        #region PropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// <locDE><para />Tritt auf, wenn sich ein Eigenschaftswert ändert.</locDE>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion PropertyChanged

        #region NotifyPropertyChanged

        /// <summary>
        /// Notifies about a property change.
        /// <locDE><para />Benachrichtigt über eine Eigenschaftswertänderung.</locDE>
        /// </summary>
        /// <param name="propertyName">The name of the property.<locDE><para />Der Name der Eigenschaft.</locDE></param>
        /// <param name="propertyNamesChangedAlso">The property names which have changed also (optional).
        /// <locDE><para />Weitere Eigenschaftsnamen, die sich ebenfalls geändert haben (optional).</locDE></param>
        protected void NotifyPropertyChanged(string propertyName, params string[] propertyNamesChangedAlso)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    handler(this, new PropertyChangedEventArgs(null));
                    return;
                }

                handler(this, new PropertyChangedEventArgs(propertyName));

                if (null != propertyNamesChangedAlso)
                {
                    foreach (string name in propertyNamesChangedAlso)
                    {
                        handler(this, new PropertyChangedEventArgs(name));
                    }
                }
            }
        }

        #endregion NotifyPropertyChanged

        #region DateTime setter

        /// <summary>
        /// Sets the specified field with a new DateTime value, fires PropertyChanged event.
        /// <locDE><para />Setzt einen neuen DateTime Wert in das angegebene Feld, löst PropertyChanged Ereignis aus.</locDE>
        /// </summary>
        /// <remarks>
        /// Specific DateTime setter as .NET doesn't detect timezone / UTC date differences.
        /// <locDE><para />Spezifischer DateTime Setter, nachdem .NET die Zeitzone bzw. die Kennzeichnung UTC nicht richtig auswertet.</locDE>
        /// </remarks>
        /// <param name="field">The reference to the private/backing field of the property.<locDE><para />Die Referenz auf das private Feld der Eigenschaft.</locDE></param>
        /// <param name="value">The new DateTime value.<locDE><para />Der neue DateTime Wert.</locDE></param>
        /// <param name="propertyName">The name of the property.<locDE><para />Der Name der Eigenschaft.</locDE></param>
        /// <param name="sendChanging">Should PropertyChanging event be raised?<locDE><para />Soll ein PropertyChanging-Ereignis ausgelöst werden?</locDE></param>
        /// <param name="propertyNamesChangedAlso">The property names which have changed also (optional).
        /// <locDE><para />Weitere Eigenschaftsnamen, die sich ebenfalls geändert haben (optional).</locDE></param>
        /// <returns>True if the value has changed.<locDE><para />True falls sich der Wert geändert hat.</locDE></returns>
        protected bool Set(ref DateTime field, DateTime value, string propertyName, bool sendChanging = false, params string[] propertyNamesChangedAlso)
        {
            // In .NET 4.5, the attribute CallerMemberName may be used: [CallerMemberName] propertyName = null

            if (field.Kind != value.Kind || !EqualityComparer<DateTime>.Default.Equals(field, value))
            {
                System.Diagnostics.Debug.Assert(null != propertyName, "Property name may not be null!");
                //if (null == propertyName)
                //{
                //    // Works, but might be a preformance bottleneck
                //    var stackTrace = new System.Diagnostics.StackTrace();
                //    var thisFrame = stackTrace.GetFrame(1);
                //    var methodName = thisFrame.GetMethod().Name;    // Should be get_* or set_*
                //    propertyName = methodName.Substring(4);
                //}

                //if (sendChanging)
                //{
                //	NotifyPropertyChanging(propertyName, propertyNamesChangedAlso);
                //}
                field = value;
                NotifyPropertyChanged(propertyName, propertyNamesChangedAlso);
                return true;
            }
            return false;
        }

        #endregion DateTime setter

        #region General setter

        /// <summary>
        /// Sets the specified field with a new value, fires PropertyChanged event.
        /// <locDE><para />Setzt einen neuen Wert in das angegebene Feld, löst PropertyChanged Ereignis aus.</locDE>
        /// </summary>
        /// <typeparam name="T">The type of the T.<locDE><para />Generischer Datentyp T.</locDE></typeparam>
        /// <param name="field">The reference to the private/backing field of the property.<locDE><para />Die Referenz auf das private Feld der Eigenschaft.</locDE></param>
        /// <param name="value">The new value.<locDE><para />Der neue Wert.</locDE></param>
        /// <param name="propertyName">The name of the property.<locDE><para />Der Name der Eigenschaft.</locDE></param>
        /// <param name="sendChanging">Should PropertyChanging event be raised?<locDE><para />Soll ein PropertyChanging-Ereignis ausgelöst werden?</locDE></param>
        /// <param name="propertyNamesChangedAlso">The property names which have changed also (optional).
        /// <locDE><para />Weitere Eigenschaftsnamen, die sich ebenfalls geändert haben (optional).</locDE></param>
        /// <returns>True if the value has changed.<locDE><para />True falls sich der Wert geändert hat.</locDE></returns>
        protected bool Set<T>(ref T field, T value, string propertyName, bool sendChanging = false, params string[] propertyNamesChangedAlso)
        {
            // In .NET 4.5, the attribute CallerMemberName may be used: [CallerMemberName] propertyName = null

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                System.Diagnostics.Debug.Assert(null != propertyName, "Property name may not be null!");
                //if (null == propertyName)
                //{
                //    // Works, but might be a preformance bottleneck
                //    var stackTrace = new System.Diagnostics.StackTrace();
                //    var thisFrame = stackTrace.GetFrame(1);
                //    var methodName = thisFrame.GetMethod().Name;    // Should be get_* or set_*
                //    propertyName = methodName.Substring(4);
                //}

                //if (sendChanging)
                //{
                //	NotifyPropertyChanging(propertyName, propertyNamesChangedAlso);
                //}
                field = value;
                NotifyPropertyChanged(propertyName, propertyNamesChangedAlso);
                return true;
            }
            return false;
        }

        #endregion General setter
    }
}