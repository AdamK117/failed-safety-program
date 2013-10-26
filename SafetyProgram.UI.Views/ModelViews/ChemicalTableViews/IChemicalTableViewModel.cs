using SafetyProgram.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
{
    /// <summary>
    /// Defines a ViewModel for the chemicaltable view.
    /// </summary>
    public interface IChemicalTableViewModel : 
        INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the header of the chemicaltable.
        /// </summary>
        string Header { get; }

        /// <summary>
        /// Get the chemicals in the chemicaltable.
        /// </summary>
        IEnumerable<Models.CoshhChemical> Chemicals { get; }
    }
}
