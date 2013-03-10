using SafetyProgram.UserControls;

namespace SafetyProgram.ICommands
{
    public class DeleteSelectedICommand : DOMBase
    {
        public DeleteSelectedICommand()
        {
            coshhWindow.Document.SelectionChanged +=new Data.DOM.CoshhDocument.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
        }

        void currentlyOpen_SelectionChangedEvent(object selection)
        {
            if (canExecute != (selection != null))
            {
                canExecute = (selection != null);
                RaiseCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            if (canExecute) { coshhWindow.Document.Selected().Remove(); }
        }
    }
}
