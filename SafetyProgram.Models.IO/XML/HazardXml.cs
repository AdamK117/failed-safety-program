using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
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
            throw new NotImplementedException();
        }
    }
}
