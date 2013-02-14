using System.Collections.Generic;

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

        private IList<HazardModel> hazards;
        public IList<HazardModel> Hazards
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
