namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels.ViewModelInterface

type RibbonViewModel(model, tabFactory) = 
    let mutable currentModel = model

    let ribbonTabGenerator doc =
        match doc with
        | Some doc -> doc |> tabFactory
        | None -> null

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    interface IViewModel<Option<Document>> with
        member this.Model = currentModel
        member this.PushModel(newModel) = 
            currentModel <- newModel
            propertyChangedEvent.Trigger(
                this,
                new PropertyChangedEventArgs("RibbonTabs"))
        member this.CommandRequested = 
            commandRequest.Publish

    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabGenerator currentModel
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.RibbonTabs = ribbonTabGenerator currentModel
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish