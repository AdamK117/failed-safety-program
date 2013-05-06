using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    public interface IDocumentICommands : ICommandsHolder
    {
        ICommand DeleteIDocObject { get; }
        ICommand InsertChemicalTable { get; }
    }
}
