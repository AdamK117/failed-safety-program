using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    public interface IWindowCommands : ICommandsHolder
    {    
        ICommand New { get; }
        ICommand Open { get; }
        ICommand Save { get; }
        ICommand SaveAs { get; }
        ICommand Close { get; }
        ICommand Exit { get; }
        ICommand Undo { get; }
        ICommand Redo { get; }
    }
}
