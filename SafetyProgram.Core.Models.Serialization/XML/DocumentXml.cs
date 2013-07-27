using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    /// <summary>
    /// Defines an implementation for an XML-Object (de)serializer.
    /// </summary>
    public sealed class DocumentXml : IStorageConverter<IDocument, XElement>
    {
        /// <summary>
        /// Construct an instance of a document XML (de)serializer.
        /// </summary>
        public DocumentXml()
        { }

        /// <summary>
        /// Serialize the document into an XML format for storage.
        /// </summary>
        /// <param name="data">The document object to be serialized.</param>
        /// <returns>The serialized document object in an XML format.</returns>
        public XElement Store(IDocument data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a document in an XML format.
        /// </summary>
        /// <param name="data">A document represented in XML.</param>
        /// <returns>The deserialized document object.</returns>
        public IDocument Load(XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
