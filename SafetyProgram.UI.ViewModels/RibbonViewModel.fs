namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.Core
open System.Windows.Input
open NewDocumentCommand
open SafetyProgram.UI.ViewModels.Core

type RibbonViewModel(model, tabFactory) = 

    let mutable currentModel = model

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()    

    let ribbonTabGenerator (mo : Option<Document * DataType>) =
        match mo with
        | Some(m, _) -> tabFactory m
        | None -> Seq.empty

    // Explicit KernelData viewmodel impl.
    interface IViewModel<KernelData> with

        member this.PushModel(newModel) = 
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Content <> oldModel.Content then
                raisePropChanged propertyChangedEvent this "RibbonTabs"           

        member this.CommandRequested = commandRequest.Publish

    interface IRibbonViewModel with
        member this.RibbonTabs = 
            ribbonTabGenerator currentModel.Content
            |> Seq.map fst

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

        member this.OpenDocument = 
            let tun f = 
                commandRequest.Trigger(f)
            new NewDocument(tun) :> ICommand

    member this.RibbonTabs = ribbonTabGenerator currentModel.Content
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = 
        let tun f = 
            commandRequest.Trigger(f)
        new NewDocument(tun) :> ICommand