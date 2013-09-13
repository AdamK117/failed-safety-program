using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a ViewModel for an insert ribbon.
    /// </summary>
    internal interface IInsertRibbonTabViewModel
    {
        /// <summary>
        /// Get a set of commands that can act on the document.
        /// </summary>
        IDocumentICommands Commands { get; }
    }
}
