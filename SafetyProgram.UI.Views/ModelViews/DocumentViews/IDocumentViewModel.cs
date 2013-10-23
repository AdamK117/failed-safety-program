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
        decimal Width { get; }

        decimal Height { get; }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        IEnumerable<Control> DocumentObjects { get; }
    }
}
