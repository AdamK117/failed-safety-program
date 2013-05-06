using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Static;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class HazardModelObject : BaseINPC, IHazardModelObject
    {
        private readonly List<string> validationErrorList = new List<string>();

        public HazardModelObject()
        { }

        public HazardModelObject(IHazardModelObject data)
        {
            hazard = data.Hazard;
            signalWord = data.SignalWord;
            symbol = data.Symbol;
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

        public void LoadData(XElement data)
        {
            //Required: Get the hazard statement for this hazard.
            {
                if (!String.IsNullOrWhiteSpace(data.Value))
                {
                    Hazard = data.Value;
                }
                else throw new InvalidDataException("No hazard was found inside a hazard statement (every hazard statement must state its hazard).");
            }

            //Optional: Get the hazards signal word. Custom hazards may not have a signal word.
            {
                var signalWordAttr = data.Attribute("signalword");
                SignalWord = (signalWordAttr == null) ? (null) : (signalWordAttr.Value);
            }

            //Optional: Get the symbol associated with the hazard. Not every hazard has a symbol.
            {
                var symbolAttr = data.Attribute("symbol");
                Symbol = (symbolAttr == null) ? (null) : (symbolAttr.Value);
            }
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return new XElement(XmlNodeNames.HazardModelObj, Hazard,
                    (SignalWord == null) ? (null) : (new XAttribute("signalword", SignalWord)),
                    (Symbol == null) ? (null) : (new XAttribute("symbol", Symbol))
                );
            }
            else throw new InvalidDataException("Errors found during save: " + Error);
        }

        public string Identifier { get { return XmlNodeNames.HazardModelObj; } }

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
            var hazardModel = new HazardModelObject();

            //Copy value fields
            hazardModel.SignalWord = signalWord;
            hazardModel.Symbol = symbol;
            hazardModel.Hazard = hazard;

            return hazardModel;
        }

        public string ComIdentity
        {
            get { return "HazardModel"; }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(ComIdentity);
        }

        public override string ToString()
        {
            return hazard;
        }
    }
}
