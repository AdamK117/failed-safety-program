namespace SafetyProgram.Models.DataModels
{
    public class CoshhProcessModel : ProcessModel
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