module SafetyProgram.UI.RibbonViewGenerators

open SafetyProgram.Core.Models
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews

let docRibbonTabUiFactory _ = 
    let vm = new InsertRibbonTabViewModel()
    let v = InsertRibbonTabView(vm)
    Seq.empty

let docObjectRibbonTabFactory = function
    | ChemicalTable(cTable) ->
        let vm = new ChemicalTableRibbonTabViewModel(cTable)
        let v = new ChemicalTableContextualRibbonTab(vm)
        v, vm