using System;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands
{
    internal sealed class CloseICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public CloseICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;

            this.applicationKernel.DocumentChanged +=
                (sender, newDocument) => CanExecute(newDocument);
        }

        public bool CanExecute(object parameter)
        {
            return applicationKernel != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            applicationKernel.Document = null;
        }
    }
}
