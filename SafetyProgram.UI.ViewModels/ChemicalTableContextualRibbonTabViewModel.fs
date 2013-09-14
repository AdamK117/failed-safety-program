namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews

type ChemicalTableRibbonTabViewModel(model, configuration, commandInvoker, selectionManager) = 
    let mutable search = ""
    let searchResult = new ObservableCollection<IChemical>()
    let commands = new ChemicalTableICommands(model, commandInvoker)
    let propertyChangedEvent = new Event<_,_>();

    interface IChemicalTableRibbonViewModel with
        member this.Search 
            with get () = search
            and set value = search<-value
        member this.SearchResult = searchResult
        member this.Commands = commands :> IChemicalTableCommands
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Search 
            with get () = search
            and set value = search<-value
    member this.SearchResult = searchResult
    member this.Commands = commands :> IChemicalTableCommands
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

