using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;
using System.Windows.Input;

namespace SafetyProgram.Commands
{
    internal sealed class NewICom<TContent> : ICommand
    {
        private readonly IWindow<TContent> window;

        /// <summary>
        /// Constructs an instance of the "New Document" command.
        /// </summary>
        /// <param name="window">Window in which the new document will be added when called.</param>
        public NewICom(IWindow<TContent> window)
        {
            this.window = window;
            this.window.ServiceChanged += (sender, newProperty) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// Can only execute if the current service allows the creation of new documents
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return window.Service.CanNew() ? true : false;
        }

        /// <summary>
        /// Attempts to create a new CoshhDocument.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called when CanExecute == false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (window.Content != null)
                {
                    try
                    {
                        window.Service.Disconnect();
                        window.Content = default(TContent);
                    }
                    catch(ArgumentException)
                    {
                        throw;
                    }                    
                }
                window.Content = window.Service.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }

        public event EventHandler CanExecuteChanged;
    }
}