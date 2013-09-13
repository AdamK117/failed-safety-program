using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.ICommands
{
    public sealed class ChemicalTableICommands : IChemicalTableCommands
    {
        public ChemicalTableICommands(IChemicalTable chemicalTable, ICommandInvoker commandInvoker)
        {
        }

        public ICommand DeleteSelected { get; private set; }

        public ICommand CopySelected { get; private set; }

        public ICommand PasteChemicals { get; private set; }

        public ICommand InsertChemical { get; private set; }
    }
}
