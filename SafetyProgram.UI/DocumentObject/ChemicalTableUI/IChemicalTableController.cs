using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a controller for a chemical table.
    /// </summary>
    public interface IChemicalTableController : IDocumentObjectUiController
    {
        /// <summary>
        /// Get the underlying ChemicalTable model.
        /// </summary>
        new IChemicalTable Model { get; }
    }
}
