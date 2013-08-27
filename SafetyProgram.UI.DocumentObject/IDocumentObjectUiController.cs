using System.Windows.Controls;
using Fluent;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    public interface IDocumentObjectUiController
    {
        /// <summary>
        /// Get the view associated with the document object.
        /// </summary>
        Control View { get; }

        /// <summary>
        /// Get the contextual ribbon tab associated with the document object.
        /// </summary>
        RibbonTabItem ContextualTab { get; }

        /// <summary>
        /// Get the underlying IDocumentObject model.
        /// </summary>
        IDocumentObject Model { get; }
    }
}
