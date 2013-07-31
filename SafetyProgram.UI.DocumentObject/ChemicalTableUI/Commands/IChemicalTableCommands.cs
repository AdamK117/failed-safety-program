using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal interface IChemicalTableCommands
    {
        ICommand DeleteSelected { get; }
        ICommand CopySelected { get; }
        ICommand PasteChemicals { get; }
        ICommand InsertChemical { get; }

        List<InputBinding> Hotkeys { get; }
    }
}
