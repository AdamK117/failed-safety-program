using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class InsertChemicalICom : ICommand
    {
        private readonly IChemicalTable chemicalTable;
        private readonly ICommandInvoker commandInvoker;

        public InsertChemicalICom(
            IChemicalTable chemicalTable, 
            ICommandInvoker commandInvoker
            )
        {
            if (chemicalTable != null && commandInvoker != null)
            {
                this.chemicalTable = chemicalTable;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //Repository will contain ChemicalModels; however, we need a CoshhChemicalModel (extended form)
                var chemicalToAdd = new CoshhChemicalObject(0M, "", (IChemicalModelObject)parameter);
                var command = new InsertChemicalInvokedCom(chemicalTable, chemicalToAdd);
                commandInvoker.InvokeCommand(command);
            }
        }
    }
}
