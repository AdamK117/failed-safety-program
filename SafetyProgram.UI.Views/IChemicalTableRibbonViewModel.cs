using SafetyProgram.UI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SafetyProgram.UI.Views
{
    /// <summary>
    /// Defines a ViewModel for a cheimcal table ribbon view.
    /// </summary>
    public interface IChemicalTableRibbonViewModel : 
        INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the current search (for searching chemicals)
        /// </summary>
        string Search { get; set; }

        /// <summary>
        /// Get the search result yielded from the search string.
        /// </summary>
        ObservableCollection<GuiChemical> SearchResult { get; }
    }
}
