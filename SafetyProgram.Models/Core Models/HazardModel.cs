using SafetyProgram.BaseClasses;
using System;

namespace SafetyProgram.Models.DataModels
{
    [Serializable]
    public class HazardModel : BaseINPC
    {
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
