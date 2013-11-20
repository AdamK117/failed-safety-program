namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open System
open SafetyProgram.Base

type Undo(commandController : ICommandController) as this =

    let canExecuteChanged = Event<_,_>()

    let handler = commandController.CanUndoChanged.Subscribe(fun _ ->
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = commandController.CanUndo()

        // Close the old document, open a new one using the IOService
        member this.Execute(_) = 
            commandController.Undo()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            handler.Dispose()