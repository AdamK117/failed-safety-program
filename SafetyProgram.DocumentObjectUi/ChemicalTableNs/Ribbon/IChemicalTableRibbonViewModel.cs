using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.Ribbon
{
    /// <summary>
    /// Defines an interface for the viewmodel of the chemical tables ribbon.
    /// </summary>
    internal interface IChemicalTableRibbonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the current search string.
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// Get the search results yielded from the search string.
        /// </summary>
        ObservableCollection<IChemical> SearchResult { get; }

        /// <summary>
        /// Get the commands available to the chemical table.
        /// </summary>
        IChemicalTableCommands Commands { get; }
    }
}
