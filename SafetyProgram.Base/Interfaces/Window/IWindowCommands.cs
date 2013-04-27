using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Base.Interfaces
{
    public interface IWindowCommands
    {    
        ICommand New { get; }
        ICommand Open { get; }
        ICommand Save { get; }
        ICommand SaveAs { get; }
        ICommand Close { get; }
        ICommand Exit { get; }

        List<InputBinding> Hotkeys { get; }
    }
}
