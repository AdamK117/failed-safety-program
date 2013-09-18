namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open System.Collections.ObjectModel
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews

type ChemicalTableViewModel(model : ChemicalTable, provider : IEvent<ChemicalTable>) as this = 

    let propertyChangedEvent = new Event<_,_>()
    let mutable chemicalTable = model
    
    do
        // Update UI if model changes.
        provider.Add(fun newModel ->
                        // Reassign to new model.
                        chemicalTable<-newModel
                        propertyChangedEvent.Trigger(
                            this,
                            new PropertyChangedEventArgs("Header"))
                        propertyChangedEvent.Trigger(
                            this,
                            new PropertyChangedEventArgs("Chemicals")))


    interface IChemicalTableViewModel with
        member this.Header = chemicalTable.Header
        member this.Chemicals = chemicalTable.Chemicals
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    // Expose implicitly.
    member this.Header = chemicalTable.Header
    member this.Chemicals = chemicalTable.Chemicals
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish
    