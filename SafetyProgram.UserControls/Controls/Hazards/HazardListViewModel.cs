using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.Controls.Hazards
{
    public class HazardListViewModel
    {
        private IEnumerable<HazardModel> hazards;
        public void ViewModel(IEnumerable<HazardModel> hazards)
        {
            this.hazards = hazards;
        }

        public IEnumerable<HazardModel> Model
        {
            get { return hazards; }
        }
    }
}
