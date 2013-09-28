namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.DocumentViews
open SafetyProgram.UI.ViewModels.ViewModelInterface

type DocumentViewModel(model, documentObjectViewFactory) = 
    let mutable currentModel = model
    
    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()
                            
    interface IViewModel<Document> with
        member this.Model = currentModel
        member this.PushModel(newModel) = 
            if newModel.Format <> currentModel.Format then
                propertyChangedEvent.Trigger(
                    this,
                    new PropertyChangedEventArgs("Format"))
            else ()
            currentModel <- newModel
            propertyChangedEvent.Trigger(
                this,
                new PropertyChangedEventArgs("DocumentObjects"))
            
        member this.CommandRequested =   
            commandRequest.Publish   
    
    // Define viewmodel links
    interface IDocumentViewModel with
        member this.Format = currentModel.Format
        
        member this.DocumentObjects =
            model.Content
            |> Seq.map documentObjectViewFactory

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Format = currentModel.Format
    
    member this.DocumentObjects = 
        model.Content
        |> Seq.map documentObjectViewFactory

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish