using SafetyProgram.Core.Commands;

namespace SafetyProgram.Document.Ribbons
{
    /// <summary>
    /// Defines a ViewModel for an insert ribbon.
    /// </summary>
    public interface IInsertRibbonTabViewModel
    {
        /// <summary>
        /// Get a set of commands that can act on the document.
        /// </summary>
        IDocumentICommands Commands { get; }
    }
}
