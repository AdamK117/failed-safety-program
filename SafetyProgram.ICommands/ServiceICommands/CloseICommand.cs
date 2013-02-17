using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class CloseICommand : ActiveDataICommandsBase
    {
        public CloseICommand()
        {
            currentlyOpen.IsOpenChangedEvent +=new Data.CoshhFile.CurrentlyOpen.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            canExecute = currentlyOpen.IsOpen();
            RaiseCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            currentlyOpen.Close();
        }
    }
}
