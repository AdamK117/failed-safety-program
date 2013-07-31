using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal interface IChemicalTableRibbonViewModel : INotifyPropertyChanged
    {
        string Search { get; set; }
        ObservableCollection<IChemical> SearchResult { get; }
        IChemicalTableCommands Commands { get; }
    }
}
