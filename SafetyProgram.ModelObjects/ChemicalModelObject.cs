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
            this.hazards = hazards;
        }

        public ChemicalModelObject(IChemicalModelObject data)
        {
            name = data.Name;
            if (data.Hazards != null)
            {
                hazards = data.Hazards;
            }
            else hazards = new ObservableCollection<IHazardModelObject>();
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

        private ObservableCollection<IHazardModelObject> hazards;
        public ObservableCollection<IHazardModelObject> Hazards
        {
            get { return hazards; }
        }

        public IChemicalModelObject LoadFromXml(XElement data)
        {
            string loadedName;
            var loadedHazards = new ObservableCollection<IHazardModelObject>();

            //Required: Get the name. A chemical must have a name.
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
                    var hazardElements = hazardsElement.Elements(XmlNodeNames.HazardModelObj);

                    //Redundancy check: Files shouldn't really be saved with empty <hazards></hazards> tags (although it won't cause a problem if it does).
                    Debug.Assert(
                        hazardElements.Count() > 0,
                        "WARNING: Empty <hazards></hazards> tags with no <hazard> in them found"
                    );
                    //End redundancy check

                    foreach (XElement hazardData in hazardElements)
                    {
                        var hazardObject = new HazardModelObject().LoadFromXml(hazardData);
                        loadedHazards.Add(hazardObject);
                    }
                }
            }

            return new ChemicalModelObject(loadedName, loadedHazards);
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return new XElement(XmlNodeNames.ChemicalModelObj,
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

        public string Identifier { get { return XmlNodeNames.ChemicalModelObj; } }

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
            ChemicalModelObject chemModel = new ChemicalModelObject();

            //Clone fields
            chemModel.Name = name;

            //Reference types, these are deep cloned into the new object from this.
            foreach (IHazardModelObject hazard in hazards)
            {
                chemModel.Hazards.Add(hazard.DeepClone());
            }

            return chemModel;
        }

        public string ComIdentity
        {
            get { return "ChemicalModel"; }
        }

        public IDataObject GetDataObject()
        {
            return this.GetDataObject(ComIdentity);
        }
    }
}
