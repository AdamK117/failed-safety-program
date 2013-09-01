using System;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    /// <summary>
    /// Defines an implementation of an IConfiguration<-->XML (de)serializer.
    /// </summary>
    public sealed class ConfigurationXml : IStorageConverter<IApplicationConfiguration, XElement>
    {
        /// <summary>
        /// Construct an instance of an IConfiguration<-->XML (de)serializer.
        /// </summary>
        public ConfigurationXml()
        { }

        /// <summary>
        /// Serialize the in-memory <code>IApplicationConfiguration</code> to an XML format.
        /// </summary>
        /// <param name="data">The <code>IApplicationConfiguration</code> to be serialized.</param>
        /// <returns>The XML representation of the <code>IApplicationConfiguration</code>.</returns>
        public XElement Store(IApplicationConfiguration data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize an XML representation of <code>IApplicationConfiguration</code> into memory.
        /// </summary>
        /// <param name="data">The XML data to be deserialized.</param>
        /// <returns>The deserialized IConfiguration object.</returns>
        public IApplicationConfiguration Load(XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
