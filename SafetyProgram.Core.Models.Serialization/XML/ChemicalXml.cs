using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    /// <summary>
    /// Implements an IXmlDomIO converter for converting between XML stored data and IChemical model.
    /// </summary>
    public sealed class ChemicalXml : IStorageConverter<IChemical, XElement>
    {
        /// <summary>
        /// Serialize the chemical object into an XML format.
        /// </summary>
        /// <param name="data">The chemical to serialize.</param>
        /// <returns>The serialized chemical in an XML format.</returns>
        public XElement Store(IChemical data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a chemical from an XML format.
        /// </summary>
        /// <param name="data">The XML data to deserialize.</param>
        /// <returns>A deserialized chemical object.</returns>
        public IChemical Load(XElement data)
        {
            //Declare variables that are populated when reading the XML
            string loadedName;
            var loadedHazards = new ObservableCollection<IHazard>();

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
                    var hazardElements = hazardsElement.Elements("hazard");

                    foreach (XElement hazardData in hazardElements)
                    {
                        var hazardFactory = new HazardXml();
                        var hazardObject = hazardFactory.Load(hazardData);
                        loadedHazards.Add(hazardObject);
                    }
                }
            }

            //Return the fully populated object
            return new Chemical(loadedName, loadedHazards);
        }
    }
}
