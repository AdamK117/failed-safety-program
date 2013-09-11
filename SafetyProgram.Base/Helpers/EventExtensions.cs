using System;
using System.ComponentModel;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines a set of extension methods that act as syntatic sugar for raising events.
    /// </summary>
    public static class EventExtensions
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T e)
            where T : EventArgs
        {

        }

        /// <summary>
        /// Raises a new PropertyChanged event, used in conjuction with INotifyPropertyChanged classes.
        /// </summary>
        /// <param name="handler">The PropertyChangedEventHandler (required by classes implementing INotifyPropertyChanged).</param>
        /// <param name="sender">The sender of the property changed event (usually 'this').</param>
        /// <param name="propertyName">The paramatername of the property being changed on the class.</param>
        public static void Raise(this PropertyChangedEventHandler handler, object sender, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Standard event raiser, acts as syntatic sugar for the null check normally required when raising an event.
        /// </summary>
        /// <param name="handler">The eventhandler to be raised.</param>
        /// <param name="sender">The sender of the event trigger (usually 'this').</param>
        public static void Raise(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises an event handler for a GenericPropertyChangedEventArg. This acts as syntatic sugar over having to fully declare the generic (which would be required for the general Raise).
        /// </summary>
        /// <typeparam name="TPropertyChanged">The type of property the GenericPropertyChangedEventArg handles.</typeparam>
        /// <param name="handler">An event that uses the GenericPropertyChangedEventArg handler</param>
        /// <param name="sender">The object raising the event (usually 'this').</param>
        /// <param name="newProperty">The new property.</param>
        public static void Raise<TPropertyChanged>(
            this EventHandler<
                GenericPropertyChangedEventArg<
                    TPropertyChanged>> handler, 
            object sender, 
            TPropertyChanged newProperty)
        {
            if (handler != null)
            {
                var eventArg = new GenericPropertyChangedEventArg<TPropertyChanged>(newProperty);
                handler(sender, eventArg);
            }
        }
    }
}
