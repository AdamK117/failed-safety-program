using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.DocumentCommands
{
    internal sealed class InsertChemicalTableICommand : ICommand
    {
        private readonly IDocument document;
        private readonly ICommandInvoker commandInvoker;

        public InsertChemicalTableICommand(IDocument document, 
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(document, commandInvoker);

            this.document = document;
            this.commandInvoker = commandInvoker;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            document.Content.Add(
                new ChemicalTable(
                        "Some Chemical Table",
                        new ObservableCollection<ICoshhChemical>()));
        }
    }
}
