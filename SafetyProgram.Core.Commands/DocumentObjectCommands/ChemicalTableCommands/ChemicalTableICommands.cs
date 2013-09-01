using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class ChemicalTableICommands : IChemicalTableCommands
    {
        public ChemicalTableICommands(IChemicalTable chemicalTable, ICommandInvoker commandInvoker)
        {
            Hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>();
        }

        public ICommand DeleteSelected { get; private set; }

        public ICommand CopySelected { get; private set; }

        public ICommand PasteChemicals { get; private set; }

        public ICommand InsertChemical { get; private set; }

        public List<InputBinding> Hotkeys { get; private set; }
    }
}
