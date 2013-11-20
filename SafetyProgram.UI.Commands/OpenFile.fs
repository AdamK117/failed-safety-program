namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base.FSharp.Helpers

type OpenFile(kernelData : GuiKernelData) =

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IOService
        member this.Execute(_) = 
            
            // CLOSE OR SAVE HERE

            kernelData.Content <- maybeBuilder {

                let dialogOutput = "NYI"

                let! doc, fs = 
                    match kernelData.Service with
                    | LocalSvc s ->
                        s.Load(dialogOutput)
                        |> Async.RunSynchronously

                return (new GuiDocument(doc), LocalFile(Some dialogOutput, Some fs))
            }                

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish