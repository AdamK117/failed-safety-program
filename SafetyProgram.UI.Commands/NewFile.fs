namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base.Helpers
open SafetyProgram.Base
open System.Collections.ObjectModel

type NewFile(kernelData : GuiKernelData) = 

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always create a new document (at the moment).
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IoService.
        member this.Execute(_) = 

            let doc, fs = 
                match kernelData.Service with
                | LocalSvc s -> 
                    s.New()
                    |> Async.RunSynchronously

            let dataType = 
                match fs with
                | Some x -> LocalFile(None, Some x)
                | None -> BufferedFile

            match doc with
            | Some x -> 
                kernelData.Content <- Some ({ Content = new GuiDocument(x); DataType = dataType; CommandController = new CommandController(); Selection = new ObservableCollection<obj>()})
            | None -> 
                // New Document was not made (error occured).
                ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish