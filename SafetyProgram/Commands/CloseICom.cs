using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;
using System.Windows.Input;

namespace SafetyProgram.Commands
{
    public sealed class CloseICom : ICommand
    {
        private readonly IWindow<IDocument> data;

        public CloseICom(IWindow<IDocument> window)
        {
            this.data = window;
            //Monitor changes in the CoshhWindow's CoshhDocument. Closed (null) documents can't be closed.
            window.ContentChanged += (document) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// May only close a document if there is one actually open
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {            
            return data.Content == null ? false : true;
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
                    data.Service.Close(data.Content);
                    data.Content = null;
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
