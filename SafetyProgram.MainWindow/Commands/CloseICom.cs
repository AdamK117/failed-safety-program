using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class CloseICom<TContent> : ICommand
    {
        private readonly IEditableHolder<TContent> contentHolder;
        private readonly IHolder<IInputService<TContent>> serviceHolder;

        public CloseICom(IEditableHolder<TContent> contentHolder,
            IHolder<IInputService<TContent>> serviceHolder)
        {
            Helpers.NullCheck(contentHolder, serviceHolder);

            this.contentHolder = contentHolder;
            this.serviceHolder = serviceHolder;

            this.contentHolder.ContentChanged += (sender, newContent) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return (contentHolder.Content == null) ? false : true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //2 Scenarios:
                //  The service closes the document successfully (no data invalidation, etc.). Remove it from the CoshhWindow
                //  The service fails at closing the document (user presses cancel, data is invalid, etc.).
                try
                {
                    serviceHolder.Content.Disconnect();
                    contentHolder.Content = default(TContent);
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
