namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.DocumentViews
open SafetyProgram.Core
open SafetyProgram.UI.ViewModels.Core

type DocumentViewModel(svc, docObjectUiFactory) as this = 

    let propertyChangedEvent = new Event<_,_>()
    
    let mutable currentModel = 
        svc.Current()
        |> Async.RunSynchronously 

    do 
        svc.DataChanged.Add(fun newModel ->
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Format <> oldModel.Format then
                raisePropChanged propertyChangedEvent this "Format"
            else ()

            raisePropChanged propertyChangedEvent this "DocumentObjects")
    
    // Define viewmodel links
    interface IDocumentViewModel with
        member this.Width = currentModel.Format.Width / 0.01m<m>
        member this.Height = currentModel.Format.Height / 0.001m<m>
        
        member this.DocumentObjects =
            currentModel.Content
            |> Seq.map (fun m ->
                let (v, _) = docObjectUiFactory m
                v)

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Format = currentModel.Format
    
    member this.DocumentObjects = 
        currentModel.Content
        |> Seq.map (fun m ->
            let (v, _) = docObjectUiFactory m
            v)

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish