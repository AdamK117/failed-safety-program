namespace SafetyProgram.Models.DataModels
{
    public class CoshhApparatusModel : ApparatusModel
    {
        private string usageComments;
        public string UsageComments
        {
            get { return usageComments; }
            set
            {
                usageComments = value;
                RaisePropertyChanged("UsageComments");
            }
        }
    }
}