using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base;
using System.Windows;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class CoshhChemicalObject : BaseINPC, ICoshhChemicalObject
    {
        private decimal _value;
        private string unit;
        private IChemicalModelObject chemical;
        private readonly List<string> validationErrorList;

        public CoshhChemicalObject()
        {
            chemical = new ChemicalModelObject();
            validationErrorList = new List<string>();
        }

        public CoshhChemicalObject(ICoshhChemicalObject data)
        {
            _value = data.Value;
            unit = data.Unit;
            chemical = data.Chemical;
        }

        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");                
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                RaisePropertyChanged("Unit");
            }
        }

        public IChemicalModelObject Chemical
        {
            get
            {
                return chemical;
            }
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
            //TODO: Validation checks pre-writing
            return
                new XElement("coshhchemical",
                    new XElement("amount", Value, new XAttribute("unit", Unit)),
                    Chemical.WriteToXElement()
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
            DataObject dataObject = new DataObject();

            dataObject.SetData(ComIdentity, DeepClone());

            return dataObject;
        }

        public override string ToString()
        {
            return chemical.Name;
        }
    }
}
