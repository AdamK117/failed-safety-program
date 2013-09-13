namespace TEMP.ViewModels

open System
open System.ComponentModel
open SafetyProgram.UI.Views.MainView.Default
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.Documents.Default
open SafetyProgram.Base

type DocumentViewModel(model : IDocument, documentObjectViewFactory) as this = 
    let mutable format = model.Format

    let documentObjects = 
        model
            .Content
            .EchoCollection(documentObjectViewFactory)

    let propertyChangedEvent = new Event<_,_>()

    do
        model.FormatChanged.Add(fun e ->
            format<-e.NewProperty
            propertyChangedEvent.Trigger(
                this,
                new PropertyChangedEventArgs("Format")))
    
    // Define viewmodel links
    interface IDocumentViewModel with
        member this.Format = format
        member this.DocumentObjects = documentObjects
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish