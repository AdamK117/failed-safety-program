module SafetyProgram.UI.ContentViewGenerators

open SafetyProgram.Core.Models
open System.Windows.Controls
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews

// Generate a view from the supplied document object model.
let documentObjectViewGenerator documentObject documentObjectChangedEvent = 
    match documentObject with
        | ChemicalTable chemicalTable -> new DefaultChemicalTableView(
                                            new ChemicalTableViewModel(
                                                chemicalTable, documentObjectChangedEvent)) :> Control

// Generate a view from the supplied document model.
let documentViews document documentChangedEvent =
    new DocumentView(
        new DocumentViewModel(
            document, documentChangedEvent, documentObjectViewGenerator, documentChangedEvent))


