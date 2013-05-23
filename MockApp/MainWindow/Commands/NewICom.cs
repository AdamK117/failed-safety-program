using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Commands
{
    internal sealed class NewICom<TContent> : ICommand
    {
        private readonly IHolderT<IIOService<TContent>> contentService;
        private readonly IEditableHolderT<TContent> contentHolder;

        public NewICom(IHolderT<IIOService<TContent>> contentService, 
            IEditableHolderT<TContent> contentHolder)
        {
            if (contentService == null ||
                contentHolder == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.contentService = contentService;
                this.contentHolder = contentHolder;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                contentHolder.Content = contentService.Content.New();
            }
            else throw new InvalidOperationException();
        }
    }
}
