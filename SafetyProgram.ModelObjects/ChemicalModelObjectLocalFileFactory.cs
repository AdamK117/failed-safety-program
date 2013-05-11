using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.ModelObjects
{
    public class ChemicalModelObjectLocalFileFactory 
        : ILocalFileFactory<IChemicalModelObject>
    {
        public static IChemicalModelObject StaticCreateNew()
        {
            return new ChemicalModelObject();
        }

        public IChemicalModelObject CreateNew()
        {
            return StaticCreateNew();
        }

        public static IChemicalModelObject StaticLoad(XElement data)
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
                    var hazardElements = hazardsElement.Elements(HazardModelObjectLocalFileFactory.XML_IDENTIFIER);

                    //Redundancy check: Files shouldn't really be saved with empty <hazards></hazards> tags (although it won't cause a problem if it does).
                    Debug.Assert(
                        hazardElements.Count() > 0,
                        "WARNING: Empty <hazards></hazards> tags with no <hazard> in them found"
                    );
                    //End redundancy check

                    foreach (XElement hazardData in hazardElements)
                    {
                        var hazardObject = HazardModelObjectLocalFileFactory.StaticLoad(hazardData);
                        loadedHazards.Add(hazardObject);
                    }
                }
            }

            //Return the fully populated object
            return new ChemicalModelObject(loadedName, loadedHazards);
        }

        public IChemicalModelObject Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(IChemicalModelObject item)
        {
            var hazardFactory = new HazardModelObjectLocalFileFactory();
            if (String.IsNullOrWhiteSpace(item.Error))
            {
                return new XElement(XML_IDENTIFIER,
                    new XElement("name", item.Name),
                    item.Hazards.Count > 0 ?
                        new XElement("hazards",
                            from hazard in item.Hazards
                            select hazardFactory.Store(hazard)
                        )
                    :
                        null
                );
            }
            else throw new InvalidDataException("Errors found when trying to save the document");
        }

        public XElement Store(IChemicalModelObject item)
        {
            return StaticStore(item);
        }

        public const string XML_IDENTIFIER = "chemical";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
