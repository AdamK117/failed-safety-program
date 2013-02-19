using SafetyProgram.UserControls;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.ICommands
{
    public class EditChemicalICommand : ActiveDataICommandsBase
    {
        public EditChemicalICommand()
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
            if (canExecute)
            {
                IDocDataHolder<CoshhChemicalModel> b = parameter as IDocDataHolder<CoshhChemicalModel>;
                EditCoshhChemical a = new EditCoshhChemical(b.Data());
                a.ShowDialog();
            }
        }
    }
}
