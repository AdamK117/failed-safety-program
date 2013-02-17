using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class SaveAsICommand : ActiveDataICommandsBase
    {
        public SaveAsICommand() : base()
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
            if (CanExecute(parameter))
            {
                currentlyOpen.SaveAs();
            }
        }
    }
}
