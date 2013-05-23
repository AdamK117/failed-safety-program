using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Commands
{
    public interface IMainWindowCommands : ICommandsHolder
    {
        ICommand New { get; }
    }
}
