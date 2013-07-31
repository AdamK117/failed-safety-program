using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal interface IChemicalTableViewModel : 
        INotifyPropertyChanged
    {
        string Header { get; set; }
        ContextMenu ContextMenu { get; }
        ObservableCollection<ICoshhChemical> Chemicals { get; }
        ObservableCollection<ICoshhChemical> SelectedChemicals { get; }
        List<InputBinding> Hotkeys { get; }
    }
}
