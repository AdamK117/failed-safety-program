using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands
{
    public sealed class ChemicalTableCommands : IChemicalTableCommands
    {
        public ChemicalTableCommands(ICommandInvoker commandInvoker, IChemicalTable chemicalTable)
        {
        }

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

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
