using System;
using System.Collections.Generic;
using System.Windows;
using SafetyProgram.Base;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class HazardModelObject : BaseINPC, IHazardModelObject
    {
        private readonly List<string> validationErrorList = new List<string>();

        public HazardModelObject()
        { }

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
                RaisePropertyChanged("Hazard");
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

        public string Error
        {
            get
            {
                if (validationErrorList.Count > 0)
                {
                    return String.Join(", ", validationErrorList);
                }
                else return null;
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
                }

                return null;
            }
        }

        public IHazardModelObject DeepClone()
        {
            //All members are value types. Construct a new instance from them
            return new HazardModelObject(signalWord, hazard, symbol);
        }

        public const string COM_IDENTITY = "HazardModel";

        public string ComIdentity
        {
            get 
            {
                return COM_IDENTITY; 
            }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(COM_IDENTITY);
        }

        public override string ToString()
        {
            return hazard;
        }
    }
}
