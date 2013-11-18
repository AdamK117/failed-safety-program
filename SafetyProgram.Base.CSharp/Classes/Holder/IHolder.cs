using System;

namespace SafetyProgram.Base.CSharp.Interfaces
{
    /// <summary>
    /// Defines a holder. A class that contains a mutable holder. When the
    /// contents of the holder changes a propertychanged event will notify
    /// observers.
    /// </summary>
    /// <typeparam name="TContent">The type of content held by the holder.</typeparam>
    public interface IHolder<out TContent>
    {
        /// <summary>
        /// Get the content of the holder.
        /// </summary>
        TContent Content { get; }

        /// <summary>
        /// Occurs when the content of the holder changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                object>> ContentChanged; 
    }
}
