namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open System.Collections.ObjectModel

type ChemicalTableRibbonViewModel() = 
    let propertyChanged = new Event<_,_>()
    let mutable search = ""
    let searchResult = new ObservableCollection<_>()

    interface IChemicalTableRibbonViewModel with
        member this.Search 
            with get () = search
            and set(x) = search<-x
        member this.SearchResult = searchResult
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

