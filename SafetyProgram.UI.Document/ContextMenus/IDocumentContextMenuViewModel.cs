using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a ViewModel for a document context menu.
    /// </summary>
    public interface IDocumentContextMenuViewModel
    {
        /// <summary>
        /// Get commands that act on the document.
        /// </summary>
        IDocumentICommands Commands { get; }
    }
}
