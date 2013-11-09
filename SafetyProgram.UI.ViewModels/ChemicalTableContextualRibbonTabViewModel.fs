namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.ViewModels.Core
open SafetyProgram.Core

type ChemicalTableRibbonTabViewModel(svc) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable currentModel = svc.Current() |> Async.RunSynchronously

    let mutable search = ""
    let searchResult = Seq.empty    

    do 
        svc.DataChanged.Add(fun newModel ->
            currentModel <- newModel)        

    interface IChemicalTableRibbonViewModel with
        member this.Search 
            with get () = search
            and set value = search<-value
        member this.SearchResult = searchResult
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Search 
            with get () = search
            and set value = search<-value
    member this.SearchResult = searchResult
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

