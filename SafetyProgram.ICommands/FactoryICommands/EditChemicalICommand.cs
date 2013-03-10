using SafetyProgram.UserControls;
using SafetyProgram.UserControls.DialogControls;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.ICommands
{
    public class EditChemicalICommand : DOMBase
    {
        public EditChemicalICommand()
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
            if (canExecute)
            {
                CoshhChemicalModel b = parameter as CoshhChemicalModel;
                EditCoshhChemical a = new EditCoshhChemical(b);
                a.ShowDialog();
            }
        }
    }
}
