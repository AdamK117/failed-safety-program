using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjectUi;

namespace SafetyProgram.DocumentUi
{
    /// <summary>
    /// Defines an interface for the Document Ui
    /// </summary>
    public interface IDocumentController : IViewable, IEditable
    {
        /// <summary>
        /// Get the ribbon tabs associated with the document (insert, etc.)
        /// </summary>
        ICollection<RibbonTabItem> DocumentRibbonTabs { get; }

        /// <summary>
        /// Get the contextual ribbon tabs of the document, depends on what is currently selected in the document.
        /// </summary>
        ObservableCollection<RibbonTabItem> ContextualRibbonTabs { get; }

        /// <summary>
        /// Get the content of the document.
        /// </summary>
        ObservableCollection<IDocumentObjectController> Content { get; }
    }
}
