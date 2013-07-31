using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal sealed class PasteChemicalsICom : ICommand
    {
        private readonly ICollection<ICoshhChemical> target;
        private readonly ICommandInvoker commandInvoker;

        /// <summary>
        /// Creates a command that allows pasting of CoshhChemicalModel's into a ChemicalTable
        /// </summary>
        /// <param name="table"></param>
        public PasteChemicalsICom(ICollection<ICoshhChemical> target, ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(target, commandInvoker);

            this.target = target;
            this.commandInvoker = commandInvoker;
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
                var command = new PasteChemicalsInvokedCom(target);
                commandInvoker.InvokeCommand(command);
            }     
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
