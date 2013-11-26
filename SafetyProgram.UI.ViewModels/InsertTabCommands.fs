namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open SafetyProgram.UI.Commands
open System

type InsertTabCommands (model : GuiDocument) = 

    let insertChemicalTable = new InsertChemicalTable(model)

    member this.InsertChemicalTable = insertChemicalTable

    interface IDisposable with
        member this.Dispose() =
            ()