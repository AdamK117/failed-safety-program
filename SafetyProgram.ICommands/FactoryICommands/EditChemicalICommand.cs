using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SafetyProgram.UserControls;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.ICommands
{
    public class EditChemicalICommand : ActiveDataICommandsBase
    {
        public EditChemicalICommand()
        {
            currentlyOpen.SelectionChangedEvent += new Data.ActiveCoshhData.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
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
                EditCoshhChemical a = new EditCoshhChemical(parameter as CoshhChemicalModel);
                a.ShowDialog();
            }
        }
    }
}
