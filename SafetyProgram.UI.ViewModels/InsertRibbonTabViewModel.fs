namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.DocumentViews

type InsertRibbonTabViewModel(model, commandInvoker) = 
    let commands = new DocumentICommands(
                    model,
                    commandInvoker)
    
    interface IInsertRibbonTabViewModel with
        member this.Commands = commands :> IDocumentICommands

    member this.Commands = commands :> IDocumentICommands

