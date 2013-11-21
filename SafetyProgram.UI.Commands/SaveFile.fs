namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open System

type SaveFile(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()

    let contentChanged = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You can only save if there is a file actually open.
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Save the file using the IO service.
        member this.Execute(_) = ()
//            match kernelData.Service with
//            | LocalSvc x -> 
//                match kernelData.Content with
//                | Some (doc, t) -> 
//                    match t with
//                    | LocalFile (pth, fs) -> 
//                        match pth with
//                        | Some s -> x.Save (s, fs, doc)
//                    | BufferedFile -> ()
//                | None -> ()
//                x.Save("Need Path", None, kernelData.Content)

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            contentChanged.Dispose()