using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class AddNewChemicalICom : ICommand
    {
        private readonly IChemicalTable table;
        private readonly ICommandInvoker commandInvoker;

        public AddNewChemicalICom(IChemicalTable table, ICommandInvoker commandInvoker) 
        {
            if (table != null && commandInvoker != null)
            {
                this.table = table;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        /// <summary>
        /// Can always execute
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Adds a random chemical to the ChemicalTable.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var invokedCommand = new AddNewChemicalInvokedCom(table);
                commandInvoker.InvokeCommand(invokedCommand);
            }            
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
