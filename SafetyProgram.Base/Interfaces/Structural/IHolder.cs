using System;

namespace SafetyProgram.Base.Interfaces
{
    public interface IHolder<out TContent>
    {
        TContent Content { get; }
        event Action<object, TContent> ContentChanged; 
    }
}
