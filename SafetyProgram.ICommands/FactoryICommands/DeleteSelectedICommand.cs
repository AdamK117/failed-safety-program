namespace SafetyProgram.ICommands
{
    public class DeleteSelectedICommand : ActiveDataICommandsBase
    {
        public DeleteSelectedICommand()
        {
            currentlyOpen.SelectionChangedEvent +=new Data.CoshhFile.CurrentlyOpen.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
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
            if (canExecute) { currentlyOpen.DeleteSelected(); }
        }
    }
}
