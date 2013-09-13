using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.ICommands
{
    internal sealed class SaveICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public SaveICommand(IApplicationKernel applicationKernel)
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
                    .CanLoad();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //If theres a document open, close it.
                if (applicationKernel.Document != null)
                {
                    try
                    {
                        applicationKernel.Service.Disconnect(applicationKernel.Document);
                        applicationKernel.Document = default(IDocument);
                    }
                    catch (ArgumentException)
                    {
                        //Closing the document cancelled out for some reason. Discontinue execution.
                        return;
                    }
                }

                //Try to load a CoshhDocument using the service.
                try
                {
                    applicationKernel.Document = applicationKernel.Service.Load();
                }
                catch (FileNotFoundException e)
                {
                    MessageBox.Show("File specified was not found");
                    throw;
                }
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }
    }
}
