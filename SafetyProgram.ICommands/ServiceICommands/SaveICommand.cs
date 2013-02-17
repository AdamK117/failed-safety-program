using SafetyProgram.Data.CoshhFile;

namespace SafetyProgram.ICommands
{
    public class SaveICommand : ActiveDataICommandsBase
    {
        public SaveICommand() : base()
        {
            currentlyOpen.IsOpenChangedEvent +=new CurrentlyOpen.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            canExecute = currentlyOpen.IsOpen();
            RaiseCanExecuteChanged();
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
