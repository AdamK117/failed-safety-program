using System;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class SaveAsICom<TContent> : ICommand
    {
        private readonly IHolder<TContent> contentHolder;
        private readonly IHolder<IInputService<TContent>> serviceHolder;

        public SaveAsICom(IHolder<TContent> contentHolder,
            IHolder<IInputService<TContent>> serviceHolder)
        {
            Helpers.NullCheck(contentHolder, serviceHolder);

            this.contentHolder = contentHolder;
            this.serviceHolder = serviceHolder;

            this.contentHolder.ContentChanged += (sender, newContent) => CanExecuteChanged.Raise(this);
            this.serviceHolder.ContentChanged += (sender, newService) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            if (contentHolder.Content != null &&
                serviceHolder.Content.CanSaveAs(contentHolder.Content))
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
                    serviceHolder.Content.SaveAs(contentHolder.Content);
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