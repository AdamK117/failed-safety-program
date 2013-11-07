namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.ViewModels.Core
open SafetyProgram.Core

type ChemicalTableViewModel(svc) as this = 

    let propertyChangedEvent = new Event<_,_>()     

    let mutable currentModel = svc.Current() |> Async.RunSynchronously

    do
        svc.KernelDataChanged.Add(fun newModel ->
            let oldModel = currentModel
            currentModel <- newModel
            raisePropChanged propertyChangedEvent this "Header"
            raisePropChanged propertyChangedEvent this "Chemicals")

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
    