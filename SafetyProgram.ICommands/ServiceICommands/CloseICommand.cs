using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class CloseICommand : ActiveDataICommandsBase
    {
        public CloseICommand()
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
            currentlyOpen.Service.Close();
        }
    }
}
