using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Base.Interfaces
{
    public interface ICommandsHolder
    {
        List<InputBinding> Hotkeys { get; }
    }
}
