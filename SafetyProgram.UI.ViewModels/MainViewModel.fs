namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open SafetyProgram.UI.Views.MainViews
open Microsoft.FSharp.Control

// Defines a standard implementation of a <code>IMainViewModel</code>
type MainViewModel(model : IApplicationKernel, ribbon, documentViewFactory) as this = 
    // Event that's called when property changes.
    let propertyChangedEvent = new Event<_,_>()

    let mutable contentView = documentViewFactory(model.Document)

    // Add hook that updates viewmodel when underlying model changes
    do
        model.DocumentChanged.Add(fun e -> 
            if e.NewProperty <> null then contentView<-documentViewFactory(e.NewProperty)
            else contentView<-null

            propertyChangedEvent.Trigger(
                this, 
                new PropertyChangedEventArgs("ContentView")))    

    // MEMBERS
    
    // Viewmodel implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon
        member this.ContentView = contentView
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Viewmodel clone to deal with explicit viewmodel impl.
    member this.RibbonView = ribbon
    member this.ContentView = contentView
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish