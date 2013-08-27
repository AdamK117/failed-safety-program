using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    internal sealed class CloseICommand : ICommand
    {
        private readonly IApplicationKernel applicationKernel;

        public CloseICommand(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            this.applicationKernel = applicationKernel;    
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
