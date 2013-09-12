using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    internal sealed class OpenICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public OpenICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);
            this.applicationKernel = applicationKernel;

            this.applicationKernel.ServiceChanged +=
                (sender, newService) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return 
                applicationKernel
                    .Service
                    .CanLoad();
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

                try
                {
                    applicationKernel.Document = applicationKernel.Service.Load();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File specified was not found");
                    throw;
                }
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
