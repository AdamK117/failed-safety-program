namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Collections.ObjectModel

type ChemicalTableViewModel(model : GuiChemicalTable) = 

    let propertyChangedEvent = new Event<_,_>()
    let selectedChemicals = new ObservableCollection<_>()

    interface IChemicalTableViewModel with
        member this.Header 
            with get () = model.Header
            and set x = model.Header <- x

        member this.Chemicals
            with get () = model.Chemicals   
            
        member this.SelectedChemicals
            with get () = selectedChemicals

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