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

    let contentCtor model dataType = 
        Some ({ 
                Content = new GuiDocument(model); 
                DataType = dataType; 
                CommandController = new CommandController(); 
                Selection = new ObservableCollection<_>()
        })

    interface ICommand with

        // You can always create a new document (at the moment).
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IoService.
        member this.Execute(_) = 

            let x = 
                match kernelData.Service with
                | LocalSvc s -> s.New() |> Async.RunSynchronously            

            do 
                match x with
                | Choice1Of2 y -> kernelData.Content <- contentCtor y BufferedData
                | Choice2Of2 err -> MessageBox.Show("Cannot create new file. ERROR: " + err) |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish