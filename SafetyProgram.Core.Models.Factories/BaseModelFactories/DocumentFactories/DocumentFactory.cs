using System.Collections.ObjectModel;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Factories
{
    /// <summary>
    /// Defines a standard implementation of the IGenerator interface for an IDocument.
    /// Generates a default IDocument with no special flags etc.
    /// </summary>
    public sealed class DocumentFactory : IGenerator<IDocument>
    {
        /// <summary>
        /// Create a new IDocument.
        /// </summary>
        /// <returns></returns>
        public IDocument CreateNew()
        {
            var formatFactory = new FormatFactory();

            return new Document(
                new ObservableCollection<IDocumentObject>(),
                formatFactory.CreateNew());
        }
    }
}
