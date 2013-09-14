module SafetyProgram.UI.ContentViewGenerators

open System
open System.Collections.Generic
open Fluent
open SafetyProgram.Base
open SafetyProgram.Core.Commands.SelectionLogic
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default
open SafetyProgram.UI.Views.ModelViews.Documents.Default
open TEMP.ViewModels
open System.Windows.Controls

let getDocumentObjectViews configuration commandInvoker selectionManager (documentObject : IDocumentObject) : Control = 
    match documentObject.Identifier with
        | ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER ->
            new ChemicalTableView(
                new ChemicalTableViewModel(
                    documentObject :?> IChemicalTable) :> IChemicalTableViewModel) :> Control
        | _ -> raise(new NotImplementedException())

let getDocumentViews configuration commandInvoker selectionManager document = 
    new DocumentView(
        new DocumentViewModel(
            document,
            getDocumentObjectViews configuration commandInvoker selectionManager) :> IDocumentViewModel) :> Control


