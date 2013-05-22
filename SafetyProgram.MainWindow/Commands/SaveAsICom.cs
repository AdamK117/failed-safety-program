using System;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class SaveAsICom<T> : ICommand
    {
        private readonly IWindow<T> window;

        /// <summary>
        /// Constructs a new ICommand that allows 'SaveAs' of the CoshhWindow's document using the CoshhWindow's service.
        /// </summary>
        /// <param name="window">Window which houses the document to be 'SavedAs'</param>
        public SaveAsICom(IWindow<T> window)
        {
            if (window != null)
            {
                this.window = window;
            }
            else throw new ArgumentNullException();            

            window.ServiceChanged += (sender, newProperty) => CanExecuteChanged.Raise(this);
            window.ContentChanged += (sender, newContent) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// Can execute if there is a CoshhDocument open to save and the CoshhWindow's service allows saving as.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (window.Content != null &&
                window.Service.CanSaveAs(window.Content))
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Attempts to save the CoshhWindow's CoshhDocument as.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called when CanExecute is false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    window.Service.SaveAs(window.Content);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Error: You do not have sufficient permissions to save to this location.");
                }
                catch (ArgumentException)
                {
                    //Saving As can cancelled for some reason, discontinue execution.
                    return;
                }
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }

        public event EventHandler CanExecuteChanged;
    }
}