using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class SaveICommand : ActiveDataICommandsBase
    {
        public SaveICommand() : base()
        {
            currentlyOpen.PropertyChanged += filePropertyChanged;
        }

        void filePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOpen")
            {
                canExecute = currentlyOpen.IsOpen();
                RaiseCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                currentlyOpen.Save();
            }
        }        
    }
}
