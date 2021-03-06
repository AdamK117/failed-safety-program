﻿namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiChemicalTable(chemicalTable) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable header = chemicalTable.Header
    let headerChanged = new Event<_>()

    let mutable selected = false
    let selectedChanged = Event<_>()

    let chemicals = 
        new ObservableCollection<_>(
            chemicalTable.Chemicals
            |> Seq.map (fun x -> new GuiCoshhChemical(x)))

    member this.Header
        with get () = header
        and set x = 
            header <- x
            raisePropChanged propertyChangedEvent this "Header"
            headerChanged.Trigger header

    member this.HeaderChanged = headerChanged.Publish

    member this.Chemicals
        with get () = chemicals

    member this.Selected 
        with get () = selected
        and set x = 
            selected <- x

    member this.SelectedChanged = selectedChanged.Publish    

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module ChemicalTableHelpers = 

    let guiChemicalTableToChemicalTable (guiChemicalTable : GuiChemicalTable) = {
        Header = guiChemicalTable.Header
        Chemicals = Seq.map CoshhChemicalHelpers.guiCoshhChemicalToCoshhChemical guiChemicalTable.Chemicals
    }