using SafetyProgram.Core.Models;
namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a controller for a chemical table.
    /// </summary>
    public interface IChemicalTableUiController : IDocumentObjectUiController
    {
        new IChemicalTable Model { get; }
    }
}
