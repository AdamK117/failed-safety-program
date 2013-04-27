using System;
using System.Windows.Input;

namespace SafetyProgram.UserControls.TagList
{
    public sealed class RemoveTagListItem : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ((TagListItem)parameter).Remove();
        }
    }
}
