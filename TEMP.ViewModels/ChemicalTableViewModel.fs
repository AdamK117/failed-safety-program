namespace TEMP.ViewModels

open System.ComponentModel
open System.Collections.ObjectModel
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default

type ChemicalTableViewModel(chemicalTable : IChemicalTable) as this = 
    let mutable header = chemicalTable.Header
    let chemicals = chemicalTable.Content
    let propertyChangedEvent = new Event<_,_>()

    let setHeader newHeader = 
        header<-newHeader
        propertyChangedEvent.Trigger(
            this,
            new PropertyChangedEventArgs("Header"))

    do
        chemicalTable.HeaderChanged.Add(fun e ->
            e.NewProperty |> setHeader)

    interface IChemicalTableViewModel with
        member this.Header
            with get () = chemicalTable.Header
            and set value = setHeader value
        member this.Chemicals = chemicals
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish