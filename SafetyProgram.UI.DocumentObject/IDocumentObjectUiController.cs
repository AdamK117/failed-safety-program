using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    /// <summary>
    /// Defines an interface for a UiController of an IDocumentObject.
    /// </summary>
    public interface IDocumentObjectUiController
    {
        /// <summary>
        /// Get the view associated with the document object.
        /// </summary>
        Control View { get; }

        /// <summary>
        /// Get the contextual ribbon tab associated with the document object.
        /// </summary>
        ObservableCollection<RibbonTabItem> ContextualTabs { get; }

        /// <summary>
        /// Get the underlying IDocumentObject model.
        /// </summary>
        IDocumentObject Model { get; }
    }
}
