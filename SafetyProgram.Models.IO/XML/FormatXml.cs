using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    /// <summary>
    /// Defines an implementation of an IFormat object (de)serializer.
    /// </summary>
    public sealed class FormatXml : IStorageConverter<IFormat, XElement>
    {
        /// <summary>
        /// Construct an instance of an IFormat (de)serializer.
        /// </summary>
        public FormatXml() 
        { }

        /// <summary>
        /// Serialize an IFormat object into an XML format.
        /// </summary>
        /// <param name="data">The IFormat object to be serialized.</param>
        /// <returns>The serialized IFormat object in an XML format.</returns>
        public XElement Store(IFormat data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize an IFormat object from an XML source.
        /// </summary>
        /// <param name="data">The XML representation of the IFormat object.</param>
        /// <returns>The deserialized IFormat object.</returns>
        public IFormat Load(System.Xml.Linq.XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
