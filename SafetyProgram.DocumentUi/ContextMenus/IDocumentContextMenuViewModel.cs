using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.DocumentUi.ContextMenus
{
    /// <summary>
    /// Defines an interface for the Document context menu view.
    /// </summary>
    public interface IDocumentContextMenuViewModel
    {
        /// <summary>
        /// Get the document commands.
        /// </summary>
        IDocumentICommands Commands { get; }
    }
}
