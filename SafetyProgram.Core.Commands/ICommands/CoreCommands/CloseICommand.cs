using System;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands.ICommands
{
    internal sealed class CloseICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public CloseICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;

            this.applicationKernel
                .DocumentChanged +=
                    (s, e) => 
                        CanExecuteChanged
                        .Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return
                applicationKernel.Document != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(null))
            {
                applicationKernel
                    .Service
                    .Disconnect(
                        applicationKernel.Document);

                applicationKernel.Document = null;
            }
            else throw new InvalidOperationException();
        }
    }
}
