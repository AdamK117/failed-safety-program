using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class ChemicalTableICommands : IChemicalTableCommands
    {
        public ChemicalTableICommands(IChemicalTable chemicalTable, ICommandInvoker commandInvoker)
        { }

        public System.Windows.Input.ICommand DeleteSelected
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Input.ICommand CopySelected
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Input.ICommand PasteChemicals
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Input.ICommand InsertChemical
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.Generic.List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
