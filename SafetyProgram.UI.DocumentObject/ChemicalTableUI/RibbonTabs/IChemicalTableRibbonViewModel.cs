using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a ViewModel for a cheimcal table ribbon view.
    /// </summary>
    internal interface IChemicalTableRibbonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the current search (for searching chemicals)
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// Get the search result yielded from the search string.
        /// </summary>
        ObservableCollection<IChemical> SearchResult { get; }

        /// <summary>
        /// Get a group of commands that act on the chemicaltable.
        /// </summary>
        IChemicalTableCommands Commands { get; }
    }
}
