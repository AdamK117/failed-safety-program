module SafetyProgram.UI.RibbonViewGenerators

open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews
open SafetyProgram.UI.ViewModels.ViewModelInterface

/// <summary>
/// Get ribbon tab views associated with the document view.
///</summary>
let docRibbonTabUiFactory _ = 
    let vm = new InsertRibbonTabViewModel()
    let v = InsertRibbonTabView(vm)
    Seq.empty

/// <summary>
/// Generate a ribbon tab from the supplied document object model.
/// </summary>
let docObjectRibbonTabFactory m = 
    match m with
    | ChemicalTable(cTable) ->
        let vm = new ChemicalTableRibbonTabViewModel(cTable)
        let v = new ChemicalTableContextualRibbonTab(vm)
        (v, vm)