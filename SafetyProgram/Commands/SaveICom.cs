using System;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    internal sealed class SaveICom<T> : ICommand
    {
        private readonly IWindow<T> window;

        /// <summary>
        /// Construct a Save command which will save the CoshhWindow's document
        /// </summary>
        /// <param name="window">CoshhWindow which houses the CoshhDocument to be saved</param>
        public SaveICom(IWindow<T> window)
        {
            if (window != null)
            {
                this.window = window;
            }
            else throw new ArgumentNullException();
            
            window.ServiceChanged += (sender, newProperty) => CanExecuteChanged.Raise(this);
            window.ContentChanged += (sender, newProperty) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// CanExecute if there is a CoshhDocument open in the CoshhWindow and the CoshhWindow's service allows saving of the document
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (window.Content != null && window.Service.CanSave(window.Content)) ? true : false;
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
                    window.Service.Save(window.Content);
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