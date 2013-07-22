using SafetyProgram.DocumentUi;
using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.Document
{
    /// <summary>
    /// Defines an interface for a factory that takes the raw (model) representation of a document and generates the suitable controller.
    /// </summary>
    public interface IDocumentFactory : IStorageConverter<IDocumentController, IDocument>
    { }
}
