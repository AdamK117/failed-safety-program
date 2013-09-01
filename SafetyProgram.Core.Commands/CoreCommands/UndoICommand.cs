using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Core.Commands
{
    internal sealed class UndoICommand : ICommand
    {
        private readonly ICommandController commandController;

        public UndoICommand(ICommandController commandController)
        {
            Helpers.NullCheck(commandController);

            this.commandController = commandController;
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
