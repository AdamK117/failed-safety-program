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

        public static IHazardModelObject ConstructFromXml(XElement data)
        {
            //Variables that are to be loaded
            string loadedHazard, loadedSignalWord, loadedSymbol;

            //Required: Get the hazard statement for this hazard.
            {
                if (!String.IsNullOrWhiteSpace(data.Value))
                {
                    loadedHazard = data.Value;
                }
                else throw new InvalidDataException("No hazard was found inside a hazard statement (every hazard statement must state its hazard).");
            }

            //Optional: Get the hazards signal word. Custom hazards may not have a signal word.
            {
                var signalWordAttr = data.Attribute("signalword");
                loadedSignalWord = (signalWordAttr == null) ? ("") : (signalWordAttr.Value);
            }

            //Optional: Get the symbol associated with the hazard. Not every hazard has a symbol.
            {
                var symbolAttr = data.Attribute("symbol");
                loadedSymbol = (symbolAttr == null) ? ("") : (symbolAttr.Value);
            }

            return new HazardModelObject(loadedHazard, loadedSignalWord, loadedSymbol);
        }
        public IHazardModelObject LoadFromXml(XElement data)
        {
            return ConstructFromXml(data);
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return new XElement(XmlNodeNames.HAZARD_MODEL_OBJ, 
                    Hazard,
                    (SignalWord == null) ? (null) : (new XAttribute("signalword", SignalWord)),
                    (Symbol == null) ? (null) : (new XAttribute("symbol", Symbol))
                );
            }
            else throw new InvalidDataException("Errors found during save: " + Error);
        }

        public string Identifier 
        { 
            get 
            { 
                return XmlNodeNames.HAZARD_MODEL_OBJ; 
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

        public string ComIdentity
        {
            get 
            { 
                return ComIdentities.HAZARD_MODEL; 
            }
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
