using System;

using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Data;

namespace SafetyProgram.ICommands
{
    public class ActiveDataICommandsBase : ICommand
    {
        protected bool canExecute;
        protected ActiveCoshhData currentlyOpen;

        public ActiveDataICommandsBase() { currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>(); }

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
