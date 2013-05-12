using System;
using System.Windows;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;
using System.Windows.Input;

namespace SafetyProgram.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    public class SaveICom : ICommand
    {
        private readonly IWindow<IDocument> data;

        /// <summary>
        /// Construct a Save command which will save the CoshhWindow's document
        /// </summary>
        /// <param name="window">CoshhWindow which houses the CoshhDocument to be saved</param>
        public SaveICom(IWindow<IDocument> window)
        {
            this.data = window;

            //Monitor if the CoshhWindow's service has changed (Service.CanSave())
            window.ServiceChanged += (service) => CanExecuteChanged.Raise(this);

            //Monitor if the CoshhWindow's document has changed (Can't save a closed (null) document)
            window.ContentChanged += (document) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// CanExecute if there is a CoshhDocument open in the CoshhWindow and the CoshhWindow's service allows saving of the document
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (data.Content != null && data.Service.CanSave(data.Content)) ? false : true;
        }

        /// <summary>
        /// Attempts to save the CoshhWindow's CoshhDocument.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    data.Service.Save(data.Content);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Error: You do not have sufficient permissions to save to this location.");
                }
                catch (ArgumentException)
                {
                    //Saving was cancelled for some reason, discontinue ICommand execution.
                    return;
                }                
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }

        public event EventHandler CanExecuteChanged;
    }
}