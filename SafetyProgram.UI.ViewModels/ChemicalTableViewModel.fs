namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.ViewModels.Core

type ChemicalTableViewModel(model) = 
    
    let mutable currentModel = model   

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()         

    interface IViewModel<ChemicalTable> with
        // Handles a new model being pushed to this viewmodel.
        member this.PushModel(newModel) = 
            currentModel <- newModel
            raisePropChanged propertyChangedEvent this "Header"
            raisePropChanged propertyChangedEvent this "Chemicals"

        // Occurs when a command is requested by a view.
        member this.CommandRequested = commandRequest.Publish

    interface IChemicalTableViewModel with
        member this.Header = currentModel.Header
        member this.Chemicals = currentModel.Chemicals
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Expose implicitly.
    member this.Header = currentModel.Header
    member this.Chemicals = currentModel.Chemicals
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish
    