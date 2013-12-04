namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open System.ComponentModel
open System
open System.Diagnostics

type CloseFile(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()
    
    let handler = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You may only close a document if there is something to close.
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Close the old document, open a new one using the IOService
        member this.Execute(_) =

            if kernelData.Content <> None then
                // Prompt to save
                kernelData.Content <- None
            else
                // Erroneous state, perhaps log
                Debug.Write 
                ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        
        member this.Dispose() = 
            handler.Dispose()