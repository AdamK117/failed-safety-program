using System.Windows.Controls;
using Fluent;

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
    }
}
