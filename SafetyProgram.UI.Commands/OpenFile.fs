namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open SafetyProgram.Base.Helpers
open SafetyProgram.Base
open System.Collections.ObjectModel
open System.Windows
open System.Windows.Controls
open System.Windows.Forms
open FSharpx.Option
open System

type OpenFile(kernelData : GuiKernelData) =

    let canExecuteChanged = Event<_,_>()

    let contentCtor model dataType = 
                Some ({ 
                        Content = new GuiDocument(model); 
                        DataType = dataType; 
                        CommandController = new CommandController(); 
                        Selection = new ObservableCollection<_>()
                })

    interface ICommand with

        // You can always open a new document
        member this.CanExecute(_) = 
            true

        // Close the old document, open a new one using the IOService
        member this.Execute(_) =
        
            // Get the users selection via a standard dialog.
            let usrSelection = 
                let files = new OpenFileDialog()
                do
                    files.Filter = "XML files (*.xml)|*.xml" |> ignore

                match files.ShowDialog() with
                | DialogResult.OK -> Some files.FileName
                | _ -> None

            // Service handler (local, db, etc.).
            let performIo pth = 
                match kernelData.Service with
                | LocalSvc s -> s.Load pth

            // Handles service response.
            let responseHandler resp = 
                match resp with
                | Choice1Of2 (doc, fileInfo) -> Some(contentCtor doc (fileInfo))
                | Choice2Of2 err -> 
                    MessageBox.Show("Error loading file:" + err) |> ignore
                    None

            // Compose the service steps together.
            let f = performIo >> Async.RunSynchronously >> responseHandler

            do
                maybe {
                    // Compose the dialog into the service steps.
                    let! content = usrSelection >>= f
                    return kernelData.Content <- content
                } |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish