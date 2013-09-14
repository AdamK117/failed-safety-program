namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open System.Collections.ObjectModel
open SafetyProgram.Core.Models
open SafetyProgram.Base
open System.Windows.Controls
open SafetyProgram.UI.Views.ModelViews.DocumentViews

type DocumentViewModel(model : IDocument, documentObjectViewFactory : IDocumentObject->Control) as this = 
    let mutable format = model.Format

    //TODO: LINKED COLLECTION
    let documentObjects = new LinkedReadOnlyObservableCollection<_,_>(model.Content, fun model->documentObjectViewFactory(model))

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
        member this.DocumentObjects = documentObjects :> ReadOnlyObservableCollection<Control>
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Format = format
    member this.DocumentObjects = documentObjects :> ReadOnlyObservableCollection<Control>
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish