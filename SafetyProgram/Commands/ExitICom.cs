using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    internal sealed class ExitICom : ICommand
    {
        private readonly IWindow window;

        public ExitICom(IWindow window)
        {
            if (window != null)
            {
                this.window = window;
            }
            else throw new ArgumentNullException();  
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            window.View.Close();
        }

        public event EventHandler CanExecuteChanged;
    }
}
