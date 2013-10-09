namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels.ViewModelInterface
open SafetyProgram.Core

type RibbonViewModel(model, tabFactory : Document -> seq<IViewModel<Document> * RibbonTabItem>) = 
    let mutable currentModel = model

    let ribbonTabGenerator (content : Option<Document * DataType>) =
        match content with
        | Some(content, dataType) -> content|> tabFactory
        | None -> null

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    interface IViewModel<Option<Document * DataType>> with
        // New model visit
        member this.PushModel(newModel) = 
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel <> oldModel then
                propertyChangedEvent.Trigger(
                    this,
                    new PropertyChangedEventArgs("RibbonTabs"))            

        member this.CommandRequested = 
            commandRequest.Publish

    interface IRibbonViewModel with
        member this.RibbonTabs = 
            ribbonTabGenerator currentModel
            |> Seq.map snd
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.RibbonTabs = ribbonTabGenerator currentModel
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish