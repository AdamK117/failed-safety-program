using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a ViewModel for the chemicaltable view.
    /// </summary>
    internal interface IChemicalTableViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the header of the chemicaltable.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Get the context menu for the chemicaltable.
        /// </summary>
        ContextMenu ContextMenu { get; }

        /// <summary>
        /// Get the chemicals in the chemicaltable.
        /// </summary>
        ObservableCollection<ICoshhChemical> Chemicals { get; }

        /// <summary>
        /// Get the currently selected chemical in the chemicaltable.
        /// </summary>
        ObservableCollection<ICoshhChemical> SelectedChemicals { get; }

        /// <summary>
        /// Get the hotkeys that act on the chemicaltable.
        /// </summary>
        List<InputBinding> Hotkeys { get; }
    }
}
