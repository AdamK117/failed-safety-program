using System;

namespace SafetyProgram.Base.Interfaces
{
    public interface IHolder<out TContent>
    {
        TContent Content { get; }
        event EventHandler<GenericPropertyChangedEventArg<object>> ContentChanged; 
    }
}
