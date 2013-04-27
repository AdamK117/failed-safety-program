using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using SafetyProgram.Base;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class HazardModelObject : BaseINPC, IHazardModelObject
    {
        private string hazard;
        private string signalWord;
        private string symbol;

        public HazardModelObject()
        {
            hazard = "";
        }

        public HazardModelObject(IHazardModelObject data)
        {
            hazard = data.Hazard;
            signalWord = data.SignalWord;
            symbol = data.Symbol;
        }

        public string Hazard
        {
            get { return hazard; }
            set
            {
                hazard = value;
                RaisePropertyChanged("Hazard");
            }
        }

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

        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
                RaisePropertyChanged("Symbol");
            }
        }

        public void LoadData(XElement data)
        {
            //Get the hazard statement (Required: Every hazard has a statement. Example: "Highly Flammable").
            if (!String.IsNullOrWhiteSpace(data.Value))
            {
                Hazard = data.Value;
            }
            else throw new InvalidDataException("No hazard was found inside a hazard statement (every hazard statement must state its hazard).");

            //Get the hazard signal word (Optional: Custom hazards won't have a signalword).
            SignalWord = data.Attribute("signalword") == null ? null : data.Attribute("signalword").Value;

            //Get the hazard symbol (Optional: Not every hazard has a symbol).
            Symbol = data.Attribute("symbol") == null ? null : data.Attribute("symbol").Value;
        }

        public XElement WriteToXElement()
        {
            //TODO: Pre-writing validation check.
            return
                new XElement("hazard", Hazard,
                    SignalWord == null ? null : new XAttribute("signalword", SignalWord),
                    Symbol == null ? null : new XAttribute("symbol", Symbol)
                );
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        //TODO: Implement this
        public string this[string columnName]
        {
            get { return null; }
        }

        public IHazardModelObject DeepClone()
        {
            var hazardModel = new HazardModelObject();

            //Copy fields
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
            DataObject dataObject = new DataObject();

            dataObject.SetData(ComIdentity, DeepClone());

            return dataObject;
        }

        public override string ToString()
        {
            return hazard;
        }
    }
}
