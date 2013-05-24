using System;
using System.ComponentModel;

namespace SafetyProgram.Base
{
    public static class EventExtensions
    {
        public static void Raise(this PropertyChangedEventHandler handler, object sender, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void Raise(this EventHandler handler, object sender, EventArgs eventArgs)
        {
            if (handler != null)
            {
                handler(sender, eventArgs);
            }
        }

        public static void Raise(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        public static void Raise<TEventArg>(this EventHandler<TEventArg> handler, object sender, TEventArg eventArg)
            where TEventArg : EventArgs
        {
            if (handler != null)
            {
                handler(sender, eventArg);
            }
        }

        public static void Raise<TPropertyChanged>(
            this EventHandler<GenericPropertyChangedEventArg<TPropertyChanged>> handler, 
            object sender, 
            TPropertyChanged newProperty)
        {
            if (handler != null)
            {
                var eventArg = new GenericPropertyChangedEventArg<TPropertyChanged>(newProperty);
                handler(sender, eventArg);
            }
        }

        public static void Raise<TPropertyChanged>(this Action<object, TPropertyChanged> handler, object sender, TPropertyChanged newProperty)
        {
            if (handler != null)
            {
                handler(sender, newProperty);
            }
        }
    }
}
