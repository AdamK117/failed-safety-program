using Fluent;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjectUi
{
    /// <summary>
    /// Defines an interface for a Document UI Object. An object that will be shown in the document in the ui.
    /// </summary>
    public interface IDocumentObjectController : IViewable, IEditable
    {
        /// <summary>
        /// Get the contextual tab of the IDocumentObject.
        /// </summary>
        RibbonTabItem ContextualTab { get; }
    }
}
