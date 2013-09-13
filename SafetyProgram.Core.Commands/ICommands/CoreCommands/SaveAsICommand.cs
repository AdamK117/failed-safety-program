using System;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands.ICommands
{
    internal sealed class SaveAsICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public SaveAsICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;

            this.applicationKernel.DocumentChanged +=
                (sender, newDocument) => this.CanExecuteChanged.Raise(this);

            this.applicationKernel.ServiceChanged +=
                (sender, newService) => this.CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return 
                applicationKernel
                .Service
                .CanSaveAs(
                    applicationKernel.Document);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
