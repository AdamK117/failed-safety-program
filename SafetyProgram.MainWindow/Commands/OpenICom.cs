using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class OpenICom<TContent> : ICommand
    {
        private readonly IEditableHolder<TContent> contentHolder;
        private readonly IHolder<IOutputService<TContent>> serviceHolder;

        public OpenICom(IEditableHolder<TContent> contentHolder,
            IHolder<IOutputService<TContent>> serviceHolder)
        {
            if (contentHolder == null ||
                serviceHolder == null)
                throw new ArgumentNullException();
            else
            {
                this.contentHolder = contentHolder;
                this.serviceHolder = serviceHolder;

                this.serviceHolder.ContentChanged += (sender, newService) => CanExecuteChanged.Raise(this);
            }
        }

        public bool CanExecute(object parameter)
        {
            return (serviceHolder.Content.CanLoad()) ? true : false;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //If theres a document open, close it.
                if (contentHolder.Content != null)
                {
                    try
                    {
                        serviceHolder.Content.Disconnect();
                        contentHolder.Content = default(TContent);
                    }
                    catch (ArgumentException)
                    {
                        //Closing the document cancelled out for some reason. Discontinue execution.
                        return;
                    }                    
                }

                //Try to load a CoshhDocument using the service.
                try
                {
                    contentHolder.Content = serviceHolder.Content.Load();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File specified was not found");
                    throw;
                }               
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");          
        }

        public event EventHandler CanExecuteChanged;
    }
}
