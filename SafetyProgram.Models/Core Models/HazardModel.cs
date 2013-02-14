namespace SafetyProgram.Models.DataModels
{
    public class HazardModel : BaseINPC
    {
        public HazardModel(string hazard) { this.hazard = hazard; }
        public HazardModel() { }

        private string hazard;
        public string Hazard
        {
            get { return hazard; }
            set 
            { 
                hazard = value;
                RaisePropertyChanged("Hazard");
            }
        }

        private string signalWord;
        public string SignalWord
        {
            get { return signalWord; }
            set 
            { 
                signalWord = value;
                RaisePropertyChanged("SignalWord");
            }
        }

        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                RaisePropertyChanged("Symbol");
            }
        }
    }
}
