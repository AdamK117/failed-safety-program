using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SafetyProgram.Models.DataModels
{
    public class BaseElementModel : BaseINPC
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private ObservableCollection<HazardModel> hazards;
        public ObservableCollection<HazardModel> Hazards
        {
            get { return hazards; }
            set
            {
                hazards = value;
                RaisePropertyChanged("Hazards");
            }
        }
    }
}
