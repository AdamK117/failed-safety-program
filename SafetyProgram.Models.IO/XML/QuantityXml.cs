using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    /// <summary>
    /// Defines an implementation for an IQuantity-XML (de)serializer.
    /// </summary>
    public sealed class QuantityXml : IStorageConverter<IQuantity, XElement>
    {
        /// <summary>
        /// Construct an instance of an IQuantity-XML (de)serializer.
        /// </summary>
        public QuantityXml()
        { }

        /// <summary>
        /// Serialize an IQuantity object into an XML format.
        /// </summary>
        /// <param name="data">The IQuantity object to be serialized.</param>
        /// <returns>The serialized IQuantity object in an XML format.</returns>
        public XElement Store(IQuantity data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize an XML representation of an IQuantity object.
        /// </summary>
        /// <param name="data">The XML data to be deserialized.</param>
        /// <returns>The deserialized IQuantity object.</returns>
        public IQuantity Load(System.Xml.Linq.XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
