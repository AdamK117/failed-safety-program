namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Collections.ObjectModel
open System.ComponentModel

type IChemicalTableRibbonTabViewModel = 
    
    inherit IViewModel

    abstract Search : string with get, set
    abstract SearchResult : ObservableCollection<string> with get