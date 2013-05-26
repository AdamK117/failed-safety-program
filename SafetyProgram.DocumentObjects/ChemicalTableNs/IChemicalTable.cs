using System.Collections.ObjectModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public interface IChemicalTable : IDocumentObject
    {
        string Header { get; }
        ObservableCollection<ICoshhChemicalObject> Chemicals { get; }
    }
}
