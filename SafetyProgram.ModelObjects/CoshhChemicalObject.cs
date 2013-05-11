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
    public sealed class CoshhChemicalObject : BaseINPC, ICoshhChemicalObject
    {
        private readonly List<string> validationErrorList = new List<string>();

        public CoshhChemicalObject()
        {
            chemical = new ChemicalModelObject();
        }

        public CoshhChemicalObject(decimal _value, string unit, IChemicalModelObject chemical)
        {
            this._value = _value;
            this.unit = unit;

            if(chemical != null)
            {
                this.chemical = chemical;
            }
            else throw new ArgumentNullException("The chemical supplied to the CoshhChemicalObject must not be null");            
        }

        private decimal _value;
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

        private string unit;
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

        private readonly IChemicalModelObject chemical;
        public IChemicalModelObject Chemical
        {
            get 
            { 
                return chemical; 
            }
        }

        public static ICoshhChemicalObject ConstructFromXml(XElement data)
        {
            decimal loadedValue;
            string loadedUnit;
            IChemicalModelObject loadedChemical;

            //Required: Get the amount of chemical being used for this CoshhChemical entry.
            {
                var amountElement = data.Element("amount");
                if (amountElement != null)
                {
                    //Parse the amount (decimal) used in this Coshh entry
                    try
                    {
                        loadedValue = decimal.Parse(amountElement.Value);
                    }
                    catch (ArgumentNullException e)
                    {
                        throw new InvalidDataException("Could not process the amount of chemical being used.", e);
                    }
                    catch (FormatException e)
                    {
                        throw new InvalidDataException("Could not parse the amount of chemical into a decimal number", e);
                    }

                    //Required: Get the units for the amount specified
                    {
                        var unitAttribute = amountElement.Attribute("unit");
                        if (unitAttribute != null)
                        {
                            loadedUnit = unitAttribute.Value;
                        }
                        else throw new InvalidDataException("No units were given for the amount of CoshhChemical being used");
                    }
                }
                else throw new InvalidDataException("No amount of the CoshhChemical was found, CoshhChemicals (not raw chemicals) need an amount");
            }

            //Required: Get the chemicals details
            {
                var chemicalElement = data.Element(XmlNodeNames.CHEMICAL_MODEL_OBJ);
                if (chemicalElement != null)
                {
                    loadedChemical = ChemicalModelObject.ConstructFromXml(chemicalElement);
                }
                else throw new InvalidDataException("No chemical was defined for the CoshhChemical");
            }

            return new CoshhChemicalObject(loadedValue, loadedUnit, loadedChemical);
        }
        public ICoshhChemicalObject LoadFromXml(XElement data)
        {
            return ConstructFromXml(data);
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return
                    new XElement(XmlNodeNames.COSHH_CHEMICAL_MODEL_OBJ,
                        new XElement("amount", 
                            Value, 
                            new XAttribute("unit", Unit)
                        ),
                        Chemical.WriteToXElement()
                    );
            }
            else throw new InvalidDataException("Errors found during save: " + Error);
        }

        public string Identifier 
        { 
            get 
            { 
                return XmlNodeNames.COSHH_CHEMICAL_MODEL_OBJ; 
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
            //Pass value types into new Ctor. DeepClone IDeepCloneable members
            return new CoshhChemicalObject(
                _value, 
                unit, 
                chemical.DeepClone()
            );
        }

        public string ComIdentity
        {
            get 
            { 
                return ComIdentities.COSHH_CHEMICAL; 
            }
        }

        public IDataObject GetDataObject()
        {
            //Uses an IStorable extension method
            return this.GetDataObject(ComIdentity);
        }

        public override string ToString() 
        { 
            return chemical.Name; 
        }
    }
}
