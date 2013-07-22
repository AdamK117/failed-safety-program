using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SafetyProgram.Models.IO.XML
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
            throw new NotImplementedException();
        }
    }
}
