using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs
{
    /// <summary>
    /// Define an interface for the Chemical Table Viewmodel.
    /// </summary>
    internal interface IChemicalTableViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the header of the chemical table.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Get the context menu of the chemical table.
        /// </summary>
        ContextMenu ContextMenu { get; }

        /// <summary>
        /// Get the chemicals in the chemical table.
        /// </summary>
        ObservableCollection<ICoshhChemical> Chemicals { get; }

        /// <summary>
        /// Get the currently selected chemicals in the chemical table.
        /// </summary>
        ObservableCollection<ICoshhChemical> SelectedChemicals { get; }

        /// <summary>
        /// Get the hotkeys of the chemical table.
        /// </summary>
        List<InputBinding> Hotkeys { get; }
    }
}
