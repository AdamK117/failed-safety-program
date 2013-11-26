namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open System.Collections.ObjectModel
open FSharpx
open SafetyProgram.Base
open System

type NewFile(kernelData : GuiKernelData) = 

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always create a new document (at the moment).
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IoService.
        member this.Execute(_) = 

            let x = 
                match kernelData.Service with
                | LocalSvc s -> s.New() |> Async.RunSynchronously

            let contentCtor model dataType = Some ({ Content = new GuiDocument(model); DataType = dataType; CommandController = new CommandController(); Selection = new ObservableCollection<_>()})

            kernelData.Content <- match x with
                                    | Choice1Of3 (doc, fs) -> contentCtor doc (LocalFile(Choice1Of2("a", fs)))
                                    | Choice2Of3 doc -> contentCtor doc BufferedFile
                                    | Choice3Of3 err -> new NotImplementedException() |> raise

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish