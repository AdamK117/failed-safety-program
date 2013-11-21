namespace SafetyProgram.UI.ViewModels

open System.Collections.ObjectModel
open SafetyProgram.UI.Models

type ChemicalTableRibbonTabViewModel(model : GuiChemicalTable) = 

    let propertyChanged = new Event<_,_>()
    let mutable search = ""
    let searchResult = new ObservableCollection<_>()

    interface IChemicalTableRibbonTabViewModel with

        member this.Search 
            with get () = search
            and set x = search <- x

        member this.SearchResult = searchResult

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Dispose() = ()

    // IMPLICIT IMPL
    member this.Search 
            with get () = search
            and set x = search <- x

    member this.SearchResult = searchResult

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Dispose() = ()