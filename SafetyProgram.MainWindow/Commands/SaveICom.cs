using System;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    internal sealed class SaveICom<TContent> : ICommand
    {
        private readonly IHolder<TContent> contentHolder;
        private readonly IHolder<IInputService<TContent>> serviceHolder;

        public SaveICom(IHolder<TContent> contentHolder,
            IHolder<IInputService<TContent>> serviceHolder)
        {
            if (contentHolder == null ||
                serviceHolder == null)
                throw new ArgumentNullException();
            else
            {
                this.contentHolder = contentHolder;
                this.serviceHolder = serviceHolder;

                serviceHolder.ContentChanged += (sender, newService) => CanExecuteChanged.Raise(this);
                contentHolder.ContentChanged += (sender, newContent) => CanExecuteChanged.Raise(this);
            }
        }

        /// <summary>
        /// CanExecute if there is a CoshhDocument open in the CoshhWindow and the CoshhWindow's service allows saving of the document
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (contentHolder.Content != null && serviceHolder.Content.CanSave(contentHolder.Content)) ? true : false;
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
                    serviceHolder.Content.Save(contentHolder.Content);
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