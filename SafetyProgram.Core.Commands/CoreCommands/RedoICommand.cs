using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Core.Commands
{
    internal sealed class RedoICommand : ICommand
    {
        private readonly ICommandInvoker commandInvoker;

        public RedoICommand(ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(commandInvoker);

            this.commandInvoker = commandInvoker;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
