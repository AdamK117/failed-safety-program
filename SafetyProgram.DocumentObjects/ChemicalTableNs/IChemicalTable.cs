using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public interface IChemicalTable :
        INotifyPropertyChanged,
        IDocumentObject
    {
        IChemicalTableCommands Commands { get; }
        IConfiguration AppConfiguration { get; }
        string Header { get; set; }
        ObservableCollection<ICoshhChemicalObject> Chemicals { get; }
        ObservableCollection<ICoshhChemicalObject> SelectedChemicals { get; }
    }
}
