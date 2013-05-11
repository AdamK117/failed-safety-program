using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Static;

namespace SafetyProgram.ModelObjects
{
    [Serializable]
    public sealed class ChemicalModelObject : BaseINPC, IChemicalModelObject
    {
        private readonly IList<string> validationErrorList = new List<string>();

        public ChemicalModelObject()
        {
            hazards = new ObservableCollection<IHazardModelObject>();
        }

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
                RaisePropertyChanged("Name");
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

        public string Error
        {
            get
            {
                if (validationErrorList.Count > 0)
                {
                    var errors = String.Join(", ", validationErrorList);
                    return errors;
                }
                else return null;
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
                        const string errorMsg = "You must enter a name for this chemical";
                        validationErrorList.Add(errorMsg);
                        return errorMsg;
                    }
                }

                //Validation for Hazards handled by internal IDataErrorInfo checks.

                return null;                
            }
        }

        public IChemicalModelObject DeepClone()
        {
            //TODO: Extension method for "this IEnumerable<T> where T : IDeepCloneable<T>"
            var hazardsOc = new ObservableCollection<IHazardModelObject>();
            foreach (IHazardModelObject hazard in hazards)
            {
                hazardsOc.Add(hazard.DeepClone());
            }

            return new ChemicalModelObject(
                name,
                hazardsOc
            );
        }

        public const string COM_IDENTITY = "ChemicalModel";

        public string ComIdentity
        {
            get
            {
                return COM_IDENTITY;
            }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(ComIdentity);
        }
    }
}
