namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Collections.ObjectModel
open System.ComponentModel

type IChemicalTableViewModel =
    
    inherit INotifyPropertyChanged

    abstract Header : string with get
    abstract Chemicals : ObservableCollection<GuiCoshhChemical> with get