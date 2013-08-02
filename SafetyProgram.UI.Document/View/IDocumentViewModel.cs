using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a viewmodel for a document view.
    /// </summary>
    public interface IDocumentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get the format of the document.
        /// </summary>
        IFormat Format { get; }

        /// <summary>
        /// Get the context menu of the document.
        /// </summary>
        ContextMenu ContextMenu { get; }

        /// <summary>
        /// Get the hotkeys of the document.
        /// </summary>
        List<InputBinding> Hotkeys { get; }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        ObservableCollection<IDocumentObjectUiController> DocumentObjects { get; }
    }
}
