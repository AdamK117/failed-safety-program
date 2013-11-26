namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Core.Models
open System

type InsertChemicalTable(document : GuiDocument) =

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IOService
        member this.Execute(_) =
            let x = { Header = "Untitled Chemical Table"; Chemicals = Seq.empty }
            let y = GuiChemicalTableDocObj(new GuiChemicalTable(x))
            document.Content.Add(y)

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            ()