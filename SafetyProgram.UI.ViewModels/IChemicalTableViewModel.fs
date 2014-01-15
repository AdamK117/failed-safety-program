namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Collections.ObjectModel
open System.ComponentModel

type IChemicalTableViewModel =
    
    inherit IViewModel

    abstract Header : string with get, set
    abstract Chemicals : ObservableCollection<GuiCoshhChemical> with get
    abstract SelectedChemicals : ObservableCollection<GuiCoshhChemical> with get