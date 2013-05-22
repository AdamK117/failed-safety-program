using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class CloseICom<TContent> : ICommand
    {
        private readonly IWindow<TContent> window;

        public CloseICom(IWindow<TContent> window)
        {
            if (window != null)
            {
                this.window = window;
                this.window.ContentChanged += (sender, newContent) => CanExecuteChanged.Raise(this);
            }
            else throw new ArgumentNullException();            
        }

        /// <summary>
        /// May only close a document if there is one actually open
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {            
            return (window.Content == null) ? false : true;
        }

        /// <summary>
        /// Closes the current document.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //2 Scenarios:
                //  The service closes the document successfully (no data invalidation, etc.). Remove it from the CoshhWindow
                //  The service fails at closing the document (user presses cancel, data is invalid, etc.).
                try
                {
                    window.Service.Disconnect();
                    window.Content = default(TContent);
                }
                catch (ArgumentException)
                {
                    throw;
                }   
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }

        public event EventHandler CanExecuteChanged;
    }
}
