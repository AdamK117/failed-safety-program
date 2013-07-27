using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    /// <summary>
    /// Defines an implementation of an IConfiguration<-->XML (de)serializer.
    /// </summary>
    public sealed class ConfigurationXml : IStorageConverter<IConfiguration, XElement>
    {
        /// <summary>
        /// Construct an instance of an IConfiguration<-->XML (de)serializer.
        /// </summary>
        public ConfigurationXml()
        { }

        /// <summary>
        /// Serialize the IConfiguration data in memory to an XML format.
        /// </summary>
        /// <param name="data">The IConfiguration data to be serialized.</param>
        /// <returns>The serialized IConfiguration data in an XML format.</returns>
        public XElement Store(IConfiguration data)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deserialize an XML representation of an IConfiguration into an IConfiguration object.
        /// </summary>
        /// <param name="data">The XML data to be deserialized.</param>
        /// <returns>The deserialized IConfiguration object.</returns>
        public IConfiguration Load(XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
