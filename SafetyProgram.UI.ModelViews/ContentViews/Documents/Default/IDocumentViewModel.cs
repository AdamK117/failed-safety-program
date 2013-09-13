using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a viewmodel for a document view.
    /// </summary>
    internal interface IDocumentViewModel : 
        INotifyPropertyChanged
    {
        /// <summary>
        /// Get the format of the document.
        /// </summary>
        IFormat Format { get; }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        ReadOnlyObservableCollection<
            IUiController> DocumentObjects { get; }
    }
}
