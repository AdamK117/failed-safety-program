using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SafetyProgram.Models.DataModels;
using System.Windows;

namespace SafetyProgram.UserControls.Controls.Hazards
{
    public class RemoveHazardICommand : ICommand
    {
        private IList<HazardModel> hazards;
        public RemoveHazardICommand(IList<HazardModel> hazards)
        {
            this.hazards = hazards;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter is HazardModel)
            {
                if (hazards.Contains(parameter as HazardModel))
                {
                    hazards.Remove(parameter as HazardModel);
                }
            }
        }
    }
}
