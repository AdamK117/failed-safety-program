using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using SafetyProgram.Base;

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

        public void LoadData(XElement data)
        {
            //Get the name (Required: A BaseElementModel must have a name)
            if (data.Element("name") != null)
            {
                Name = data.Element("name").Value;
            }
            else throw new InvalidDataException("The loaded chemical must have a name!");

            //Get the hazards (Optional: A BaseElementModel may have no hazards)
            if (data.Element("hazards") != null)
            {
                XElement hazardsData = data.Element("hazards");

                Debug.Assert(
                    hazardsData.Elements("hazard").Count() > 0,
                    "WARNING: Empty <hazards></hazards> tags with no <hazard> in them found"
                );

                foreach (XElement hazardData in hazardsData.Elements("hazard"))
                {
                    IHazardModelObject hazardObject = new HazardModelObject();
                    hazardObject.LoadData(hazardData);
                    hazards.Add(hazardObject);
                }
            }
        }

        public XElement WriteToXElement()
        {
            if (String.IsNullOrWhiteSpace(Error))
            {
                return new XElement("chemical",
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
