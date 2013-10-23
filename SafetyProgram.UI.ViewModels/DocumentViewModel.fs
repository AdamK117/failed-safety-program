namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.DocumentViews
open SafetyProgram.UI.ViewModels.ViewModelInterface
open System.Windows.Controls

type DocumentViewModel(model, docObjectUiFactory) = 

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()
    
    let mutable currentModel = model  
                            
    interface IViewModel<Document> with
        member this.PushModel(newModel) = 
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Format <> oldModel.Format then
                propertyChangedEvent.Trigger(
                    this,
                    new PropertyChangedEventArgs("Format"))
            else ()

            propertyChangedEvent.Trigger(
                this,
                new PropertyChangedEventArgs("DocumentObjects"))
            
        member this.CommandRequested =   
            commandRequest.Publish   
    
    // Define viewmodel links
    interface IDocumentViewModel with
        member this.Width = currentModel.Format.Width / 0.01m<m>
        member this.Height = currentModel.Format.Height / 0.001m<m>
        
        member this.DocumentObjects =
            model.Content
            |> Seq.map (fun m ->
                let (v, _) = docObjectUiFactory m
                v)

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Format = currentModel.Format
    
    member this.DocumentObjects = 
        model.Content
            |> Seq.map (fun m ->
                let (v, _) = docObjectUiFactory m
                v)

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish