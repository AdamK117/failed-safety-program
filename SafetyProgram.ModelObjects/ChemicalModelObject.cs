using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class ChemicalModelObject : 
        INotifyPropertyChanged, 
        IChemicalModelObject
    {
        public ChemicalModelObject(string name, ObservableCollection<IHazardModelObject> hazards)
        {
            this.name = name;

            if (hazards != null)
            {
                this.hazards = hazards;
            }
            else throw new ArgumentNullException("The hazards supplied must not be null");
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged.Raise(this, "Name");
            }
        }

        private readonly ObservableCollection<IHazardModelObject> hazards;
        public ObservableCollection<IHazardModelObject> Hazards
        {
            get 
            { 
                return hazards; 
            }
        }

        private readonly IList<string> validationErrorList = new List<string>();

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
                validationErrorList.Clear();

                //Rules for Name
                //  Every chemical has a name. Must not be null.
                if (columnName == "Name")
                {
                    if (Name.Length == 0)
                    {
                        const string ERROR_MSG = "You must enter a name for this chemical";
                        validationErrorList.Add(ERROR_MSG);
                        return ERROR_MSG;
                    }
                }

                //Validation for Hazards handled by internal IDataErrorInfo checks.

                return null;                
            }
        }

        public IChemicalModelObject DeepClone()
        {
            var hazardsOc = new ObservableCollection<IHazardModelObject>(
                this.hazards.DeepCloneList()
            );

            return new ChemicalModelObject(
                name,
                hazardsOc
            );
        }

        public const string IDENTIFIER = "ChemicalModel";

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
