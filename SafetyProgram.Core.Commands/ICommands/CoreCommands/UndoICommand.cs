using System;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands.ICommands
{
    internal sealed class UndoICommand : ICommand
    {
        private readonly ICommandController commandController;

        public UndoICommand(ICommandController commandController)
        {
            Helpers.NullCheck(commandController);

            this.commandController = commandController;

            this.commandController.CanUndoChanged +=
                (s, e) => CanExecute(null);
        }

        public bool CanExecute(object parameter)
        {
            return
                commandController
                    .CanUndo();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(null))
            {
                commandController.Undo();
            }
            else throw new InvalidOperationException();
        }
    }
}
