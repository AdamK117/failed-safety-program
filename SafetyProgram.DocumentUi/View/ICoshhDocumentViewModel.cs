using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

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
        ContextMenu ContextMenu { get; }

        /// <summary>
        /// Get the hotkeys associated with the document.
        /// </summary>
        List<InputBinding> Hotkeys { get; }
    }
}
