namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open System.Collections.ObjectModel
open FSharpx
open SafetyProgram.Base
open System.Windows.Forms
open System

type NewFile(kernelData : GuiKernelData) = 

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always create a new document (at the moment).
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IoService.
        member this.Execute(commandParam) = 

            // Close the previous file.
            use closeCommand = new CloseFile(kernelData)
            if (closeCommand :> ICommand).CanExecute(commandParam) then
                (closeCommand :> ICommand).Execute(commandParam)

            let x = 
                match kernelData.Service with
                | LocalSvc s -> s.New() |> Async.RunSynchronously         

            do 
                match x with
                | Choice1Of2 y -> kernelData.Content <- Some <| ContentHolderHelpers.defaultConstructor y BufferedData
                | Choice2Of2 err -> 
                    MessageBox.Show(
                        "Cannot create a new document. The following error was recorded: " + err,
                        "Document Creation Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1) |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish