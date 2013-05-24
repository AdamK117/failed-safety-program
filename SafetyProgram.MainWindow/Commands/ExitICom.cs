using System;
using System.Windows;
using System.Windows.Input;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class ExitICom : ICommand
    {
        private readonly Window window;

        public ExitICom(Window window)
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
            window.Close();
        }

        public event EventHandler CanExecuteChanged;
    }
}
