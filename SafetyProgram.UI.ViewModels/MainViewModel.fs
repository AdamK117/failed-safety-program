namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core
open SafetyProgram.UI.Views.MainViews
open System.Windows.Input
open NewDocumentCommand
open SafetyProgram.UI.ViewModels.Core

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(svc, ribbonView, modelUiFactory) as this = 

    let propertyChangedEvent = new Event<_,_>()

    let generateDocumentMvvm = 
        Option.bind (fun (m, _) -> 
            let v, vm = modelUiFactory m
            Some(m, v, vm))    

    let extractView = function
        | Some(_, v, _) -> v
        | None -> null

    let generateView = generateDocumentMvvm >> extractView

    let mutable currentModel = 
        svc.Current() 
        |> Async.RunSynchronously

    let mutable contentView = 
        currentModel.Content
        |> generateView

    do
        svc.DataChanged.Add(fun newModel ->
            let oldModel = currentModel
            currentModel <- newModel
            if currentModel.Content <> oldModel.Content then
                contentView <- generateView currentModel.Content
                raisePropChanged propertyChangedEvent this "ContentView")

    // Viewmodel explicit implementation
    interface IMainViewModel with
        member this.RibbonView = ribbonView

        member this.ContentView = contentView

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit
    member this.RibbonView = ribbonView

    member this.ContentView = contentView

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = new NewDocument(svc) :> ICommand