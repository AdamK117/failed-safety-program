namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open SafetyProgram.Core.Models
open System.Windows.Controls
open Fluent
open SafetyProgram.UI.Views.MainViews
open Microsoft.FSharp.Control
open SafetyProgram.UI.ViewModels.ViewModelInterface
open System.Windows.Input

type NewDocument(tun) = 

    let canExecuteChangedEvent = new Event<_,_>()

    interface ICommand with
        member this.CanExecute(param) =
            false

        member this.Execute(param) =
            let newDocFunc oldDoc = 
                None
            tun newDocFunc

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChangedEvent.Publish

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model : KernelData, ribbon, modelUiFactory) = 

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    let generateDocumentMvvm m = 
        m
        |> Option.bind (fun (m, _) -> 
            let (v, vm) = modelUiFactory m
            Some(m, v, vm))

    let mutable currentModel = model

    // ViewModel impl.
    interface IViewModel<KernelData> with

        // New model visitors
        member this.PushModel(newModel) =
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Content <> oldModel.Content then
                propertyChangedEvent.Trigger(
                    this,
                    new PropertyChangedEventArgs("ContentView"))

        // Occurs when a command is requested by the view.
        member this.CommandRequested =
            commandRequest.Publish

    // Viewmodel explicit implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon
        member this.ContentView = 
            generateDocumentMvvm currentModel.Content
            |> function
                | Some(_, v, _) -> v
                | None -> null

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit implementation
    member this.RibbonView = ribbon
    member this.ContentView =
        generateDocumentMvvm currentModel.Content
            |> function
                | Some(_, v, _) -> v
                | None -> null

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = 
        let tun f = 
            commandRequest.Trigger(f)
        new NewDocument(tun) :> ICommand