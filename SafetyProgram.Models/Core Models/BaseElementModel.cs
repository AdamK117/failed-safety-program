using System.Collections.ObjectModel;
using System;
using SafetyProgram.BaseClasses;

namespace SafetyProgram.Models.DataModels
{
    [Serializable]
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
