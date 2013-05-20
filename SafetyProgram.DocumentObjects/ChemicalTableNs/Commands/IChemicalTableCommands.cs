using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    public interface IChemicalTableCommands
    {
        ICommand DeleteSelected { get; }
        ICommand DeleteTable { get; }
        ICommand CopySelected { get; }
        ICommand PasteChemicals { get; }
        ICommand InsertChemical { get; }

        List<InputBinding> Hotkeys { get; }
    }
}
