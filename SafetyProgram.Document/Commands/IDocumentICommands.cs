using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Document.Commands
{
    public interface IDocumentICommands
    {
        ICommand DeleteIDocObject { get; }
        ICommand InsertChemicalTable { get; }

        List<InputBinding> Hotkeys { get; }
    }
}
