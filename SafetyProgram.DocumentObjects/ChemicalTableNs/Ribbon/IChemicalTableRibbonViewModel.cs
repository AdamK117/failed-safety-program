using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal interface IChemicalTableRibbonViewModel : INotifyPropertyChanged
    {
        string Search { get; set; }
        ObservableCollection<IChemicalModelObject> SearchResult { get; }
        IChemicalTableCommands Commands { get; }
    }
}
