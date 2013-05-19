using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SafetyProgram.Base;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class CoshhChemicalObject : INotifyPropertyChanged, ICoshhChemicalObject
    {
        private readonly List<string> validationErrorList = new List<string>();

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
                PropertyChanged.Raise(this, "Value");               
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
                PropertyChanged.Raise(this, "Unit");
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

        public const string COM_IDENTITY = "CoshhChemicalObject";

        public string ComIdentity
        {
            get 
            {
                return COM_IDENTITY; 
            }
        }

        public IDataObject GetDataObject()
        {
            //Uses an IStorable extension method
            return this.GetDataObject(COM_IDENTITY);
        }

        public override string ToString() 
        { 
            return chemical.Name; 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
