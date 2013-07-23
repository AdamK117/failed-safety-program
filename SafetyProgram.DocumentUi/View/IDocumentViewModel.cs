using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentUi
{
    /// <summary>
    /// Defines an interface for the viewmodel of an ICoshhDocumentView.
    /// </summary>
    public interface ICoshhDocumentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get the context menu of the document.
        /// </summary>
        ContextMenu Menu { get; }

        /// <summary>
        /// Get the hotkeys associated with the document.
        /// </summary>
        List<InputBinding> Hotkeys { get; }

        /// <summary>
        /// Get the items in the body of the document.
        /// </summary>
        ObservableCollection<Control> BodyItems { get; }

        IFormat Format { get; }
    }
}
