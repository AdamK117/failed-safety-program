namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base.Helpers
open SafetyProgram.Base
open System.Collections.ObjectModel
open FSharpx.Option
open System

type OpenFile(kernelData : GuiKernelData) =

    let canExecuteChanged = Event<_,_>()

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IOService
        member this.Execute(_) = 
            
            // CLOSE OR SAVE HERE

            kernelData.Content <- maybe {

                let dialogOutput = @"C:\Temp\CoshhFile.xml"

                let resp = 
                    match kernelData.Service with
                    | LocalSvc s ->
                        s.Load(dialogOutput)
                        |> Async.RunSynchronously

                let x =
                    match resp with
                    | Choice1Of2 (doc, fs) -> new NotImplementedException() |> raise
                    | Choice2Of2 x -> new NotImplementedException() |> raise

                return! None

                //return { Content = new GuiDocument(doc); DataType = LocalFile(Some dialogOutput, Some fs); CommandController = new CommandController(); Selection = new ObservableCollection<obj>()}
            }                

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish