namespace TEMP.ViewModels

open System
open System.ComponentModel
open SafetyProgram.UI.Views.MainView.Default
open SafetyProgram.Core
open Fluent

// Defines a standard implementation of a <code>IMainViewModel</code>
type FSViewModel(model : IApplicationKernel, ribbon, documentViewFactory) as this = 
    // Event that's called when property changes.
    let propertyChangedEvent = new Event<_,_>()

    let mutable contentView = documentViewFactory(model.Document)

    // Add hook that updates viewmodel when underlying model changes
    do
        model.DocumentChanged.Add(fun e -> 
                contentView<-documentViewFactory(e.NewProperty)
                propertyChangedEvent.Trigger(
                    this, 
                    new PropertyChangedEventArgs("ContentView")))  
    
    // Viewmodel implementation
    interface IMainViewModel with
        member this.RibbonView = ribbon        
        member this.ContentView = contentView
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish