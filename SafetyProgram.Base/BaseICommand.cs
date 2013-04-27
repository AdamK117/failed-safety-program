using System;
using System.Windows.Input;

namespace SafetyProgram.Base
{
    public abstract class BaseICommand : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        /// <summary>
        /// Event that is called if CanExecute(object) has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the CanExecuteChanged ICommand event (Redraws ICommands in the UI etc.)
        /// </summary>
        protected virtual void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
