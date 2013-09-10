using System;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    /// <summary>
    /// Defines an implementation of a IHazard (de)serializer.
    /// </summary>
    public sealed class HazardXml : IStorageConverter<IHazard, XElement>
    {
        /// <summary>
        /// Construct an instance of an IHazard (de)serializer.
        /// </summary>
        public HazardXml()
        { }
        
        /// <summary>
        /// Serialize an IHazard object into an XML format.
        /// </summary>
        /// <param name="data">The IHazard object to be serialized.</param>
        /// <returns>The serialized IHazard object in an XML format.</returns>
        public XElement Store(IHazard data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize an XML representation of an IHazard object.
        /// </summary>
        /// <param name="data">The XML data to be deserialized.</param>
        /// <returns>The deserialized IHazard object.</returns>
        public IHazard Load(System.Xml.Linq.XElement data)
        {
            //Variables that are to be loaded
            string loadedHazard, loadedSignalWord, loadedSymbol;

            //Required: Get the hazard statement for this hazard.
            {
                if (!String.IsNullOrWhiteSpace(data.Value))
                {
                    loadedHazard = data.Value;
                }
                else throw new InvalidDataException("No hazard was found inside a hazard statement (every hazard statement must state its hazard).");
            }

            //Optional: Get the hazards signal word. Custom hazards may not have a signal word.
            {
                var signalWordAttr = data.Attribute("signalword");
                loadedSignalWord = (signalWordAttr == null) ? ("") : (signalWordAttr.Value);
            }

            //Optional: Get the symbol associated with the hazard. Not every hazard has a symbol.
            {
                var symbolAttr = data.Attribute("symbol");
                loadedSymbol = (symbolAttr == null) ? ("") : (symbolAttr.Value);
            }

            return new Hazard(loadedHazard, loadedSignalWord, loadedSymbol);
        }
    }
}
