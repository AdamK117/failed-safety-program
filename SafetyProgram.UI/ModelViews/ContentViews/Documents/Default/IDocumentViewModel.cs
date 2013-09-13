using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.ModelViews.Documents.Default
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
        ReadOnlyObservableCollection<Control> DocumentObjects { get; }
    }
}
