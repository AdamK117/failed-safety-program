namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open System.Collections.ObjectModel
open SafetyProgram.Core.Models
open System.Windows.Controls
open SafetyProgram.UI.Views.ModelViews.DocumentViews

type DocumentViewModel(model : Document, provider : IEvent<Document>, documentObjectViewFactory : DocumentObject->Control) as this = 
    let mutable format = model.Format
    let mutable documentObjectViews = model.Content |> Seq.map documentObjectViewFactory
    let propertyChangedEvent = new Event<_,_>()

    do
        provider.Add(fun newDocument -> 
                        if newDocument.Format <> format then
                            format<-newDocument.Format
                            propertyChangedEvent.Trigger(
                                this,
                                new PropertyChangedEventArgs("Format"))
                        else ()
                        documentObjectViews <- newDocument.Content |> Seq.map documentObjectViewFactory                                                
                        propertyChangedEvent.Trigger(
                            this,
                            new PropertyChangedEventArgs("DocumentObjects")))        
    
    // Define viewmodel links
    interface IDocumentViewModel with
        member this.Format = format
        member this.DocumentObjects = documentObjectViews
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Format = format
    member this.DocumentObjects = documentObjectViews
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish