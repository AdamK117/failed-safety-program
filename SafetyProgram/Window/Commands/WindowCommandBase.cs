using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SafetyProgram.Window.Commands
{
    public abstract class WindowCommandBase : ICommand
    {
        protected bool canExecute;
        protected CoshhWindow window;

        public WindowCommandBase(CoshhWindow window)
        {
            this.window = window;
        }

        public virtual bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        } 
    }
}
