namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open System.ComponentModel
open System

type CloseFile(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()
    
    let handler = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Close the old document, open a new one using the IOService
        member this.Execute(_) = 
            
            // SAVE OR CLOSE HERE

            kernelData.Content <- None

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        
        member this.Dispose() = 
            handler.Dispose()