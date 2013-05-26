using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class UndoICom : ICommand
    {
        private readonly ICommandInvoker commandInvoker;

        public UndoICom(ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(commandInvoker);

            this.commandInvoker = commandInvoker;

            this.commandInvoker.CanUndoChanged += (sender, e) => CanExecuteChanged.Raise(this);          
        }

        public bool CanExecute(object parameter)
        {
            return (commandInvoker.CanUndo()) ? true : false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                commandInvoker.Undo();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
