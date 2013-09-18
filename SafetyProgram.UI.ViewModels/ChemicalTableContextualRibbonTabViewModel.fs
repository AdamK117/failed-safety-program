namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews

type ChemicalTableRibbonTabViewModel(model : ChemicalTable, provider : IEvent<ChemicalTable>) = 
    let mutable search = ""
    let searchResult = Seq.empty
    let propertyChangedEvent = new Event<_,_>()

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

