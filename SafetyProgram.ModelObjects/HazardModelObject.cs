using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    internal sealed class HazardModelObject : INotifyPropertyChanged, IHazardModelObject
    {
        private readonly List<string> validationErrorList = new List<string>();

        public HazardModelObject(string hazard, string signalWord, string symbol)
        {
            this.hazard = hazard;
            this.signalWord = signalWord;
            this.symbol = symbol;
        }

        private string hazard;
        public string Hazard
        {
            get { return hazard; }
            set
            {
                hazard = value;
                PropertyChanged.Raise(this, "Hazard");
            }
        }

        private string signalWord;
        public string SignalWord
        {
            get
            {
                return signalWord;
            }
            set
            {
                signalWord = value;
                PropertyChanged.Raise(this, "SignalWord");
            }
        }

        private string symbol;
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                PropertyChanged.Raise(this, "Symbol");
            }
        }

        public string Error
        {
            get
            {
                return ErrorValidation.JoinErrors(validationErrorList);
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    //A hazard can't be blank, must have some sort of statement
                    case "Hazard":
                        if (String.IsNullOrWhiteSpace(Hazard))
                        {
                            const string ERR_MSG_NO_HAZARD = "No hazard statement was given for a hazard";
                            validationErrorList.Add(ERR_MSG_NO_HAZARD);
                            return ERR_MSG_NO_HAZARD;
                        }
                        break;
                    //SignalWord is optional
                    //Symbol is optional
                }
                return null;
            }
        }

        public IHazardModelObject DeepClone()
        {
            return new HazardModelObject(hazard, signalWord, symbol);
        }

        public const string IDENTIFIER = "HazardModel";

        public string ComIdentity
        {
            get 
            {
                return IDENTIFIER; 
            }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(IDENTIFIER);
        }

        public override string ToString()
        {
            return hazard;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
