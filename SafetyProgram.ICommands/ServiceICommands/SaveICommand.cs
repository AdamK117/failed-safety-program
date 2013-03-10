using SafetyProgram.Data.DOM;

namespace SafetyProgram.ICommands
{
    public class SaveICommand : DOMBase
    {
        public SaveICommand() : base()
        {
            coshhWindow.Document.IsOpenChanged +=new CoshhDocument.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            canExecute = coshhWindow.Document.IsOpen();
            RaiseCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                coshhWindow.Save();
            }
        }        
    }
}
