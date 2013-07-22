using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs
{
    /// <summary>
    /// Defines an interface for a factory that takes a raw chemical table model and constructs a IDocumentObjectController from it.
    /// </summary>
    public interface IChemicalTableFactory : IDeserialize<IChemicalTableController, IChemicalTable>
    {
    }
}
