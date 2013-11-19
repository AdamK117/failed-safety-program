namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base.FSharp.Helpers

type NewDocument(kernelData : GuiKernelData) = 

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always create a new document
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IOService
        member this.Execute(_) = 

            // CLOSE or SAVE document here.

            let doc, fs = 
                match kernelData.Service with
                | LocalSvc s -> 
                    s.New()
                    |> Async.RunSynchronously

            let dataType = 
                match fs with
                | Some x -> LocalFile x
                | None -> BufferedFile

            match doc with
            | Some x -> 
                kernelData.Content <- Some (new GuiDocument(x), dataType)
            | None -> ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish