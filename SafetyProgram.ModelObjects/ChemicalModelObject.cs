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
        private string name;
        private ObservableCollection<IHazardModelObject> hazards;

        private readonly IList<string> validationErrorList;

        public ChemicalModelObject()
        {
            hazards = new ObservableCollection<IHazardModelObject>();
        }

        public ChemicalModelObject(IChemicalModelObject data)
        {
            name = data.Name;
            hazards = data.Hazards;
        }

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

        public ObservableCollection<IHazardModelObject> Hazards
        {
            get
            {
                return hazards;
            }
            set
            {
                hazards = value;
                RaisePropertyChanged("Hazards");
            }
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
            //TODO: Validation check
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

        //TODO: Implement this
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        //TODO: Implement this
        public string this[string columnName]
        {
            get { return null; }
        }

        public IChemicalModelObject DeepClone()
        {
            ChemicalModelObject chemModel = new ChemicalModelObject();

            //Clone fields
            chemModel.Name = name;

            //Reference types, these are deep cloned
            chemModel.Hazards = new ObservableCollection<IHazardModelObject>();
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
            DataObject dataObject = new DataObject();

            dataObject.SetData(ComIdentity, DeepClone());

            return dataObject;
        }
    }
}
