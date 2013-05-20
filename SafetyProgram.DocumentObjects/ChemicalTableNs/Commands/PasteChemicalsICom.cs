using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class PasteChemicalsICom : ICommand
    {
        private readonly IChemicalTable table;
        private readonly ICommandInvoker commandInvoker;

        /// <summary>
        /// Creates a command that allows pasting of CoshhChemicalModel's into a ChemicalTable
        /// </summary>
        /// <param name="table"></param>
        public PasteChemicalsICom(IChemicalTable table, ICommandInvoker commandInvoker)
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
        /// Attempts to paste CoshhChemicalModels into the ChemicalTable
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="System.OutOfMemoryException">Thrown when a non-serializable object is put on the clipboard</exception>
        /// <exception cref="System.InvalidCastException"></exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var command = new PasteChemicalsInvokedCom(table);
                commandInvoker.InvokeCommand(command);
            }     
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
