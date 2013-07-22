using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.DocumentUi.Ribbons
{
    /// <summary>
    /// Defines an interface for the CoshhDocumentRibbonTabView
    /// </summary>
    public interface ICoshhDocumentRibbonTabViewModel
    {
        /// <summary>
        /// Get the document commands.
        /// </summary>
        IDocumentICommands Commands { get; }
    }
}
