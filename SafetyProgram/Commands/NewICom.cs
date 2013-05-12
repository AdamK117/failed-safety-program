using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;
using System.Windows.Input;

namespace SafetyProgram.Commands
{
    public sealed class NewICom : ICommand
    {
        private readonly IWindow<IDocument> data;

        /// <summary>
        /// Constructs an instance of the "New Document" command.
        /// </summary>
        /// <param name="window">Window in which the new document will be added when called.</param>
        public NewICom(IWindow<IDocument> window)
        {
            this.data = window;
            //Monitor changes in the CoshhWindow's service (affects CanNew())
            window.ServiceChanged += (service) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// Can only execute if the current service allows the creation of new documents
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return data.Service.CanNew() ? true : false;
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
                if (data.Content != null)
                {
                    try
                    {
                        data.Service.Close(data.Content);
                        data.Content = null;
                    }
                    catch(ArgumentException)
                    {
                        throw;
                    }                    
                }
                data.Content = data.Service.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }

        public event EventHandler CanExecuteChanged;
    }
}