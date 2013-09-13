using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.ICommands
{
    /// <summary>
    /// Defines a standard implementation of a command that creates a
    /// new document within the application kernel.
    /// </summary>
    internal sealed class NewICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public NewICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;

            this.applicationKernel
                .ServiceChanged +=
                    (s, e) => 
                        CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return 
                this.applicationKernel
                    .Service
                    .CanNew();
        }

        /// <summary>
        /// Occurs when the <code>CanExecute</code> state of the
        /// command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Execute the command, creating a new document within the
        /// application kernel.
        /// </summary>
        /// <param name="parameter">NOT USED</param>
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
