using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public interface IChemicalTableViewModel : INotifyPropertyChanged
    {
        string Header { get; set; }
        ContextMenu ContextMenu { get; }
        ObservableCollection<ICoshhChemicalObject> Chemicals { get; }
        ObservableCollection<ICoshhChemicalObject> SelectedChemicals { get; }
        List<InputBinding> Hotkeys { get; }
        void Select();
    }
}
