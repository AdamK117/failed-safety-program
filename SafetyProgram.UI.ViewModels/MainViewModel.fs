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

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model : KernelData, ribbon, contentViewFactory : Document -> Option<IViewModel<Document> * Control>) = 

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    let generateContentViews content = 
        content
        |> Option.bind (fun (content, dataType) -> contentViewFactory content)
        |> function
            | Some(viewModel, control) -> control
            | None -> null

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
        member this.ContentView = generateContentViews <| currentModel.Content
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit implementation
    member this.RibbonView = ribbon
    member this.ContentView = generateContentViews <| currentModel.Content
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish