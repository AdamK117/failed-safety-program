using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Core;

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
        Models.Format Format { get; }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        IEnumerable<Control> DocumentObjects { get; }
    }
}
