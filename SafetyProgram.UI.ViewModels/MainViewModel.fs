namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open SafetyProgram.UI.Views.MainViews
open Microsoft.FSharp.Control
open SafetyProgram.UI.ViewModels.ViewModelInterface

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model : KernelData, ribbon, documentViewFactory) = 
    // Event that's called when property changes.
    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()

    let contentViewFactory = function
        | Some(document) -> document |> documentViewFactory
        | None -> null

    let mutable currentModel = model 

    // ViewModel impl.
    interface IViewModel<KernelData> with
        member this.Model = currentModel
        member this.PushModel(newModel) =
            currentModel <- newModel
            propertyChangedEvent.Trigger(
                this,
                new PropertyChangedEventArgs("ContentView"))
        member this.CommandRequested =
            commandRequest.Publish

    // Viewmodel explicit implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon
        member this.ContentView = contentViewFactory <| currentModel.Document
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit implementation
    member this.RibbonView = ribbon
    member this.ContentView = contentViewFactory <| currentModel.Document
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish