namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Collections.ObjectModel
open System.ComponentModel

type IChemicalTableRibbonViewModel = 
    
    inherit INotifyPropertyChanged

    abstract Search : string with get, set
    abstract SearchResult : ObservableCollection<string> with get