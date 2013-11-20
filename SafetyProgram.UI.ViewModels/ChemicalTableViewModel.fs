namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models

type ChemicalTableViewModel(chemicalTable : GuiChemicalTable) = 

    let propertyChangedEvent = new Event<_,_>()

    interface IChemicalTableViewModel with
        member this.Header 
            with get () = chemicalTable.Header

        member this.Chemicals
            with get () = chemicalTable.Chemicals

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish