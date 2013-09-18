namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open SafetyProgram.UI.Views.MainViews
open Microsoft.FSharp.Control

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model : KernelData, provider : IEvent<KernelData>, ribbon, documentViewFactory) as this =
    // Event that's called when property changes.
    let propertyChangedEvent = new Event<_,_>()

    // If a document isn't open, display a substitute (e.g. suggestions page or something)
    let contentViewFactory = function
        | Some(document) -> document |> documentViewFactory
        | None -> null

    let mutable contentView = contentViewFactory <| model.Document

    // Add hook that updates viewmodel when underlying model changes
    do
        provider.Add(fun newKernelData ->
                        contentView <- contentViewFactory <| newKernelData.Document
                        propertyChangedEvent.Trigger(
                            this,
                            new PropertyChangedEventArgs("ContentView")))

    // Viewmodel explicit implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon
        member this.ContentView = contentView
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Implicit implementation
    member this.RibbonView = ribbon
    member this.ContentView = contentView
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish