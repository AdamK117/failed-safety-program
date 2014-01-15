namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiCoshhChemical(coshhChemical) = 

    let propertyChangedEvent = new Event<_,_>()
    
    let mutable chemical = new GuiChemical(coshhChemical.Chemical)
    let chemicalChanged = new Event<_>()

    let mutable quantity = coshhChemical.Quantity
    let quantityChanged = new Event<_>()

    let mutable selected = false
    let selectedChanged = Event<_>()

    member this.Chemical
        with get () = chemical
        and set x = 
            chemical <- x
            raisePropChanged propertyChangedEvent this "Chemical"
            chemicalChanged.Trigger chemical

    member this.ChemicalChanged = chemicalChanged.Publish

    member this.Quantity
        with get () = quantity
        and set x = 
            quantity <- x
            raisePropChanged propertyChangedEvent this "Quantity"
            quantityChanged.Trigger quantity

    member this.QuantityChanged = quantityChanged.Publish

    member this.Selected 
        with get () = selected
        and set x = 
            selected <- x

    member this.SelectedChanged = selectedChanged.Publish

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module CoshhChemicalHelpers = 

    let guiCoshhChemicalToCoshhChemical (guiCoshhChemical : GuiCoshhChemical) = {
        Chemical = ChemicalHelpers.guiChemicalToChemical guiCoshhChemical.Chemical
        Quantity = guiCoshhChemical.Quantity
    }