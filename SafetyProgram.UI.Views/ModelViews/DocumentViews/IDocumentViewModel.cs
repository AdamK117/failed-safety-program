using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Views.ModelViews.DocumentViews
{
    /// <summary>
    /// Defines a viewmodel for a document view.
    /// </summary>
    public interface IDocumentViewModel : 
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
