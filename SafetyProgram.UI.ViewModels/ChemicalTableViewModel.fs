namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models

type ChemicalTableViewModel(model : GuiChemicalTable) = 

    let propertyChangedEvent = new Event<_,_>()

    interface IChemicalTableViewModel with
        member this.Header 
            with get () = model.Header
            and set x = model.Header <- x

        member this.Chemicals
            with get () = model.Chemicals            

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

        member this.Dispose() = ()

    // IMPLICIT IMPL
    member this.Header 
            with get () = model.Header
            and set x = 
                model.Header <- x

    member this.Chemicals
        with get () = model.Chemicals            

    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.Dispose() = ()