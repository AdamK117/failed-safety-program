using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    public sealed class ExitICom : ICommand
    {
        private readonly IWindow<IDocument> window;

        public ExitICom(IWindow<IDocument> window)
        {
            this.window = window;
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
