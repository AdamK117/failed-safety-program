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

        public static IChemicalModelObject ConstructFromXml(XElement data)
        {
            //Declare variables that are populated when reading the XML
            string loadedName;
            var loadedHazards = new ObservableCollection<IHazardModelObject>();

            //Required: Get the chemicals name. A chemical must have a name.
            {
                var chemicalNameElement = data.Element("name");
                if (chemicalNameElement != null)
                {
                    loadedName = chemicalNameElement.Value;
                }
                else throw new InvalidDataException("The loaded chemical must have a name!");
            }

            //Optional: Get the hazards associated with the chemical. (A chemical may have no hazards).
            {
                var hazardsElement = data.Element("hazards");
                if (hazardsElement != null)
                {
                    var hazardElements = hazardsElement.Elements(XmlNodeNames.HAZARD_MODEL_OBJ);

                    //Redundancy check: Files shouldn't really be saved with empty <hazards></hazards> tags (although it won't cause a problem if it does).
                    Debug.Assert(
                        hazardElements.Count() > 0,
                        "WARNING: Empty <hazards></hazards> tags with no <hazard> in them found"
                    );
                    //End redundancy check

                    foreach (XElement hazardData in hazardElements)
                    {
                        var hazardObject = HazardModelObject.ConstructFromXml(hazardData);
                        loadedHazards.Add(hazardObject);
                    }
                }
            }

            //Return the fully populated object
            return new ChemicalModelObject(loadedName, loadedHazards);
        }
        public IChemicalModelObject LoadFromXml(XElement data)
        {
            return ConstructFromXml(data);
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return new XElement(XmlNodeNames.CHEMICAL_MODEL_OBJ,
                    new XElement("name", Name),
                    Hazards.Count > 0 ?
                        new XElement("hazards",
                            from hazard in hazards
                            select hazard.WriteToXElement()
                        )
                    :
                        null
                );
            }
            else throw new InvalidDataException("Errors found when trying to save the document");
        }

        public string Identifier 
        { 
            get 
            {
                return XmlNodeNames.CHEMICAL_MODEL_OBJ; 
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

        public string ComIdentity
        {
            get
            {
                return ComIdentities.CHEMICAL_MODEL;
            }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(ComIdentity);
        }
    }
}
