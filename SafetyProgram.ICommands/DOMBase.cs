using System;

using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Data;
using SafetyProgram.Data.DOM;

namespace SafetyProgram.ICommands
{
    public class DOMBase : ICommand
    {
        protected bool canExecute;
        protected CoshhWindow coshhWindow;

        public DOMBase() { coshhWindow = ServiceLocator.Current.GetInstance<CoshhWindow>(); }

        public virtual bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public virtual void Execute(object parameter) { }

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
