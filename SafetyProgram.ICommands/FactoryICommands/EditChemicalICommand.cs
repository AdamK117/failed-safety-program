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
            currentlyOpen.PropertyChanged += filePropertyChanged;
        }

        void filePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                if (canExecute != (currentlyOpen.Selected != null))
                {
                    canExecute = (currentlyOpen.Selected != null);
                    RaiseCanExecuteChanged();
                }

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
