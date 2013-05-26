using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    internal sealed class NewICom<TContent> : ICommand
    {
        private readonly IEditableHolder<TContent> contentHolder;
        private readonly IHolder<IIOService<TContent>> serviceHolder;

        public NewICom(IEditableHolder<TContent> contentHolder,
            IHolder<IIOService<TContent>> serviceHolder)
        {
            Helpers.NullCheck(contentHolder, serviceHolder);

            this.contentHolder = contentHolder;
            this.serviceHolder = serviceHolder;

            this.contentHolder.ContentChanged += 
                (sender, newContent) => CanExecuteChanged.Raise(this);

            this.serviceHolder.ContentChanged += 
                (sender, newService) => CanExecuteChanged.Raise(this);
        }

        public bool CanExecute(object parameter)
        {
            return (serviceHolder.Content.CanNew()) ? true : false;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (contentHolder.Content != null)
                {
                    try
                    {
                        serviceHolder.Content.Disconnect();
                        contentHolder.Content = default(TContent);
                    }
                    catch(ArgumentException)
                    {
                        throw;
                    }                    
                }

                contentHolder.Content = serviceHolder.Content.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }

        public event EventHandler CanExecuteChanged;
    }
}