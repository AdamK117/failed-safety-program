module SafetyProgram.UI.RibbonViewGenerators

open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews

/// <summary>
/// Get ribbon tab views associated with the document view.
///</summary>
let getDocumentRibbonTabs document = 
    [ new InsertRibbonTabView(
        new InsertRibbonTabViewModel())]

/// <summary>
/// Generate a ribbon tab from the supplied document object model.
/// </summary>
let getDocumentObjectRibbonTabs (documentObject, documentChangedEvent) = 
    match documentObject with
        | ChemicalTable chemicalTable -> new ChemicalTableContextualRibbonTab(
                                            new ChemicalTableRibbonTabViewModel(
                                                chemicalTable, documentChangedEvent))



