using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;

namespace SafetyProgram.Commands
{
    internal sealed class RedoICom : ICommand
    {
        private readonly ICommandInvoker commandInvoker;

        public RedoICom(ICommandInvoker commandInvoker)
        {
            if (commandInvoker != null)
            {
                this.commandInvoker = commandInvoker;  
            }
            else throw new ArgumentNullException();

            this.commandInvoker.CanRedoChanged += (sender, e) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return commandInvoker.CanRedo() ? true : false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                commandInvoker.Redo();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
