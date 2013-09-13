using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines document UI controller specific fields.
    /// </summary>
    public interface IDocumentUiController : 
        IUiController
    {
        /// <summary>
        /// Get the document model this controller oversees.
        /// </summary>
        new IDocument Model { get; }
    }
}
