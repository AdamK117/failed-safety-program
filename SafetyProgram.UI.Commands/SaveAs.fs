namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open System.Windows.Forms
open FSharpx.Option
open System

type SaveAs(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()

    let contentChanged = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You can always save a document.
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Close the old document, open a new one using the IOService
        member this.Execute(_) =

            // Prompt the user for a save path.
            let getSavePath () = 
                let savePath = new SaveFileDialog()
                do
                    savePath.Filter <- "XML files (*.xml)|*.xml"

                match savePath.ShowDialog() with
                | DialogResult.OK -> Some savePath.FileName
                | _ -> None                

            maybe {
                let! savePath = getSavePath()
                let dataType = UnlockedFile(savePath)

                let! contentHolder = kernelData.Content
                let model = DocumentHelpers.guiDocumentToDocument contentHolder.Content            

                do
                    match kernelData.Service with
                    | LocalSvc x -> 
                        x.Save(model, dataType)
                        |> Async.RunSynchronously
                        |> function
                            | Choice1Of2 x ->
                                let newContentHolder = { contentHolder with DataType = x }
                                kernelData.Content <- Some newContentHolder
                            | Choice2Of2 err -> MessageBox.Show("Cannot create new file. ERROR: " + err) |> ignore
            } |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with
        member this.Dispose() =
            contentChanged.Dispose()