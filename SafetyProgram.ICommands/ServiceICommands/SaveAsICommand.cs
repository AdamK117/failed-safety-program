using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class SaveAsICommand : DOMBase
    {
        public SaveAsICommand() : base()
        {
            coshhWindow.Document.IsOpenChanged +=new Data.DOM.CoshhDocument.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
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
                coshhWindow.SaveAs();
            }
        }
    }
}
