using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    public sealed class CoshhChemicalXml : IStorageConverter<ICoshhChemical, XElement>
    {
        /// <summary>
        /// Serialize a coshh chemical into an XML format.
        /// </summary>
        /// <param name="data">The coshh chemical to serialize.</param>
        /// <returns>The serialized coshh chemical.</returns>
        public XElement Store(ICoshhChemical data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a coshh chemical stored in an XML format.
        /// </summary>
        /// <param name="data">The coshh chemical in an XML format.</param>
        /// <returns>The deserialized coshh chemical object.</returns>
        public ICoshhChemical Load(System.Xml.Linq.XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
