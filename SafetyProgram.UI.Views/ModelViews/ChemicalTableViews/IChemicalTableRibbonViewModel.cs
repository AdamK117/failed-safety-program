using System.Collections.Generic;
using System.ComponentModel;
using SafetyProgram.Core;

namespace SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
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
        IEnumerable<Models.Chemical> SearchResult { get; }
    }
}
