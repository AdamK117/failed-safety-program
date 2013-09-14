module SafetyProgram.UI.RibbonViewGenerators

open System
open System.Collections.Generic
open Fluent
open SafetyProgram.Base
open SafetyProgram.Core.Commands.SelectionLogic
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.RibbonTabViews.DocumentObjects.ChemicalTables
open SafetyProgram.UI.Views.ModelViews.RibbonTabViews.Documents
open TEMP.ViewModels

let getDocumentRibbonTabs commandInvoker document = 
    [new InsertRibbonTabView(
        new InsertRibbonTabViewModel(
            document,
            commandInvoker)) :> RibbonTabItem]

let getDocumentObjectRibbonTabs (documentObject : IDocumentObject) applicationConfiguration commandInvoker selectionManager = 
    match documentObject.Identifier with
        | ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER ->
            new ChemicalTableRibbonView(
                new ChemicalTableRibbonTabViewModel(
                    documentObject :?> IChemicalTable,
                    applicationConfiguration,
                    commandInvoker,
                    selectionManager))
        | _ -> raise (new NotImplementedException())



