namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base
open System

type Redo(commandController : ICommandController) as this =

    let canExecuteChanged = Event<_,_>()

    let handler = commandController.CanRedoChanged.Subscribe(fun _ ->
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // Check if there is a command to redo.
        member this.CanExecute(_) = 
            commandController.CanRedo()

        // Redo the last command
        member this.Execute(_) = 
            commandController.Redo()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            handler.Dispose()