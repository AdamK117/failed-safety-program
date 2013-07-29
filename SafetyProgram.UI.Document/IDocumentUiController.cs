using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    public interface IDocumentUiController
    {
        /// <summary>
        /// Get the view associated with the controller.
        /// </summary>
        Control View { get; }

        /// <summary>
        /// Get the ribbon tabs associated with this document type.
        /// </summary>
        ObservableCollection<RibbonTabItem> DocumentRibbonTabs { get; }

        /// <summary>
        /// Get the document ui objects contained within the document.
        /// </summary>
        ObservableCollection<IDocumentObjectUiController> DocumentObjects { get; }
    }
}
