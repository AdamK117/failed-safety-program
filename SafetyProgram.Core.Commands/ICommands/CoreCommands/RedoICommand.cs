using System;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands.ICommands
{
    internal sealed class RedoICommand : ICommand
    {
        private readonly ICommandController commandController;

        public RedoICommand(ICommandController commandController)
        {
            Helpers.NullCheck(commandController);

            this.commandController = commandController;

            this.commandController.CanRedoChanged +=
                (s, e) => CanExecute(this.commandController.CanRedo());
        }

        public bool CanExecute(object parameter)
        {
            return 
                commandController
                    .CanRedo();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                commandController.Redo();
            }
            else throw new InvalidOperationException();            
        }
    }
}
