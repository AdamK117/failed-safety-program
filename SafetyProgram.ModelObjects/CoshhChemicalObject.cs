using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using SafetyProgram.Base;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class CoshhChemicalObject : BaseINPC, ICoshhChemicalObject
    {
        private readonly List<string> validationErrorList = new List<string>();

        public CoshhChemicalObject()
        {
            chemical = new ChemicalModelObject();
        }

        public CoshhChemicalObject(ICoshhChemicalObject data)
        {
            _value = data.Value;
            unit = data.Unit;
            chemical = data.Chemical;
        }

        private decimal _value;
        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");                
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }

        private IChemicalModelObject chemical;
        public IChemicalModelObject Chemical
        {
            get { return chemical; }
            set
            {
                chemical = value;
                RaisePropertyChanged("Chemical");
            }
        }

        public void LoadData(XElement data)
        {
            //Get the amount of the chemical (Required)
            if (data.Element("amount") != null)
            {
                //Parse the amount (decimal) used in this Coshh entry
                try
                {
                    Value = decimal.Parse(data.Element("amount").Value);
                }
                catch (ArgumentNullException e)
                {
                    throw new InvalidDataException("Could not process the amount of chemical being used.", e);
                }
                catch (FormatException e)
                {
                    throw new InvalidDataException("Could not parse the amount of chemical into a decimal number", e);
                }

                //Get the units of the amount (Required)
                if (data.Element("amount").Attribute("unit") != null)
                {
                    Unit = data.Element("amount").Attribute("unit").Value;
                }
                else throw new InvalidDataException("No units were given for the amount of CoshhChemical being used");
            }
            else throw new InvalidDataException("No amount of the CoshhChemical was found, CoshhChemicals (not raw chemicals) need an amount");

            //Get the chemical details (Required)
            if (data.Element("chemical") != null)
            {
                Chemical.LoadData(data.Element("chemical"));
            }
            else throw new InvalidDataException("No chemical was defined for the CoshhChemical");
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return
                new XElement("coshhchemical",
                    new XElement("amount", Value, new XAttribute("unit", Unit)),
                    Chemical.WriteToXElement()
                );
            }
            else throw new InvalidDataException("Errors found during save: " + Error);
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
                    case "Value":
                        if (Value == 0)
                        {
                            const string ERR_MSG_NO_VALUE = "No amount of this entry has been given, there must be an amount!";
                            validationErrorList.Add(ERR_MSG_NO_VALUE);
                            return ERR_MSG_NO_VALUE;
                        }
                        break;
                    case "Unit":
                        if (String.IsNullOrWhiteSpace(unit))
                        {
                            const string ERR_MSG_NO_UNIT = "No units given for the amount of chemical, there must be units!";
                            validationErrorList.Add(ERR_MSG_NO_UNIT);
                            return ERR_MSG_NO_UNIT;
                        }
                        break;
                }

                return null;
            }
        }

        public ICoshhChemicalObject DeepClone()
        {
            var coshhModel = new CoshhChemicalObject();

            //Clone properties on the new object
            coshhModel.Value = _value;
            coshhModel.Unit = unit;

            //Reference types, needs cloning
            coshhModel.Chemical = chemical.DeepClone();

            return coshhModel;
        }

        public string ComIdentity
        {
            get { return "CoshhChemicalObject"; }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(ComIdentity);
        }

        public override string ToString() { return chemical.Name; }
    }
}
