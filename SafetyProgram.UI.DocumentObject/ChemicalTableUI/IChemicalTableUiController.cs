using System.Collections.ObjectModel;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    public interface IChemicalTableUiController : IDocumentObjectUiController
    {
        /// <summary>
        /// Get or set the header of the chemical table.
        /// </summary>
        string Header { get; set; }

        /// <summary>
        /// Get the chemicals in the chemical table.
        /// </summary>
        ObservableCollection<ICoshhChemical> Chemicals { get; }
    }
}
