using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    internal sealed class NewICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public NewICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;

            this.applicationKernel.ServiceChanged +=
                (sender, newService) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return this.applicationKernel.Service.CanNew();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (applicationKernel.Document != null)
                {
                    try
                    {
                        applicationKernel.Service.Disconnect(applicationKernel.Document);
                        applicationKernel.Document = default(IDocument);
                    }
                    catch (ArgumentException)
                    {
                        throw;
                    }
                }
                applicationKernel.Document = applicationKernel.Service.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
