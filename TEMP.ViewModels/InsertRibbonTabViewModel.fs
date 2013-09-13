namespace TEMP.ViewModels

open System
open System.ComponentModel
open SafetyProgram.UI.Views.MainView.Default
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.ModelViews.RibbonTabViews.Documents

type InsertRibbonTabViewModel(model, commandInvoker) = 
    let commands = new DocumentICommands(
                    model,
                    commandInvoker)
    
    interface IInsertRibbonTabViewModel with
        member this.Commands = commands :> IDocumentICommands

