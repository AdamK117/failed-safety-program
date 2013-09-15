module SafetyProgram.UI.ContentViewGenerators

open System
open System.Collections.Generic
open Fluent
open SafetyProgram.Base
open SafetyProgram.Core.Commands.SelectionLogic
open SafetyProgram.Core.Models
open System.Windows.Controls
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
open SafetyProgram.UI.Views.ModelViews.DocumentViews


let getDocumentObjectViews configuration commandInvoker selectionManager (documentObject : IDocumentObject) : Control = 
    match documentObject.Identifier with
        | ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER ->
            new DefaultChemicalTableView(
                new ChemicalTableViewModel(
                    documentObject :?> IChemicalTable) :> IChemicalTableViewModel) :> Control
        | _ -> raise(new NotImplementedException())

let getDocumentViews configuration commandInvoker selectionManager document = 
    new DocumentView(
        new DocumentViewModel(
            document,
            getDocumentObjectViews configuration commandInvoker selectionManager) :> IDocumentViewModel) :> Control


