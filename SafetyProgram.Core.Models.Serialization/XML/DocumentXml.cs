using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    /// <summary>
    /// Defines an implementation for an XML-Object (de)serializer.
    /// </summary>
    public sealed class DocumentXml : ILocalStorageConverter<IDocument, XElement>
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
            Helpers.NullCheck(data);

            string title;
            IFormat format = new A4Format();
            ObservableCollection<IDocumentObject> documentContent;

            var documentObjectFactory = new DocumentObjectXml();

            documentContent = new ObservableCollection<IDocumentObject>(
                from documentObject in data.Elements()
                select documentObjectFactory.Load(documentObject));

            return new Document(documentContent, format);
        }

        private string[] extensions = { "xml" };

        public IEnumerable<string> Extensions
        {
            get { return extensions; }
        }

        private const string FILE_DESCRIPTION = "Xml File";

        public string FileDescription
        {
            get { return FILE_DESCRIPTION; }
        }
    }
}
