using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class DeleteSelectedICommand : ActiveDataICommandsBase
    {
        public DeleteSelectedICommand()
        {
            currentlyOpen.PropertyChanged += filePropertyChanged;
        }

        void filePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                if (canExecute != (currentlyOpen.Selected != null))
                {
                    canExecute = (currentlyOpen.Selected != null);
                    RaiseCanExecuteChanged();
                }
                
            }
        }

        public override void Execute(object parameter)
        {
            if (canExecute) { currentlyOpen.Factory.DeleteSelected(); }
        }
    }
}
