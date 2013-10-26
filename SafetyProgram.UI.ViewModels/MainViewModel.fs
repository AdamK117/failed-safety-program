namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core
open SafetyProgram.UI.Views.MainViews
open System.Windows.Input
open NewDocumentCommand
open SafetyProgram.UI.ViewModels.Core

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model, ribbon, modelUiFactory) = 

    let mutable currentModel = model

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    let generateDocumentMvvm = 
        Option.bind (fun (m, _) -> 
            let v, vm = modelUiFactory m
            Some(m, v, vm))    

    // ViewModel impl.
    interface IViewModel<KernelData> with

        // New model visitors
        member this.PushModel(newModel) =
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Content <> oldModel.Content then
                raisePropChanged propertyChangedEvent this "ContentView"

        // Occurs when a command is requested by the view.
        member this.CommandRequested =
            commandRequest.Publish

    // Viewmodel explicit implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon

        member this.ContentView = 
            match generateDocumentMvvm currentModel.Content with
            | Some(_, v, _) -> v
            | None -> null

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit
    member this.RibbonView = ribbon

    member this.ContentView = 
        match generateDocumentMvvm currentModel.Content with
        | Some(_, v, _) -> v
        | None -> null

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = 
        let tun f = 
            commandRequest.Trigger(f)
        new NewDocument(tun) :> ICommand