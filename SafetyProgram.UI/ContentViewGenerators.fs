module SafetyProgram.UI.ContentViewGenerators

open SafetyProgram.Core.Models
open System.Windows.Controls
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews

// Generate a view from the supplied document object model.
let docObjUiFactory = function
    | ChemicalTable(cTable) ->
        let vm = new ChemicalTableViewModel(cTable)
        let v = new DefaultChemicalTableView(vm) :> Control
        (v, vm :> obj)

//// Generate a view from the supplied document model.
let docUiFactory model =
    let vm = new DocumentViewModel(model, docObjUiFactory)
    let v = new DocumentView(vm)
    (v :> Control, vm :> obj)