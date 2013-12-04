namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open FSharpx.Option
open System

type SaveFile(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()

    let contentChanged = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // A file may only be saved if one is already open.
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Save the file using the IO service.
        member this.Execute(_) =

            let content =
                match kernelData.Content with
                | Some x -> x
                | None -> new NotImplementedException() |> raise

            let model = DocumentHelpers.guiDocumentToDocument content.Content

            let dataType = 
                match content.DataType with
                | Local x -> x
                | BufferedData -> new NotImplementedException() |> raise // Prompt user for save location
            
            do
                match kernelData.Service with
                | LocalSvc x -> 
                    x.Save(model, dataType) 
                    |> Async.RunSynchronously
                    |> function
                        | Choice1Of2 x -> new NotImplementedException() |> raise // Reassign datatype et. al.
                        | Choice2Of2 err -> new NotImplementedException() |> raise // Show saving error message.           

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            contentChanged.Dispose()