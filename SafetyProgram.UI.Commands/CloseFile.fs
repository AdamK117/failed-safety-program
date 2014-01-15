namespace SafetyProgram.UI.Commands

open System.Windows.Input
open SafetyProgram.UI.Models
open SafetyProgram.Core.Services
open System.ComponentModel
open System
open System.Diagnostics
open System.Windows.Forms

type CloseFile(kernelData : GuiKernelData) as this =

    let canExecuteChanged = Event<_,_>()
    
    let handler = kernelData.ContentChanged.Subscribe(fun _ -> 
        canExecuteChanged.Trigger(this, new EventArgs()))

    interface ICommand with

        // You may only close a document if there is something to close.
        member this.CanExecute(_) = 
            match kernelData.Content with
            | Some _ -> true
            | None -> false

        // Close the old document, open a new one using the IOService
        member this.Execute(commandParam) =

            if kernelData.Content <> None then
                let resp = 
                    MessageBox.Show(
                        "Do you want to save the changes you made to the current document?",
                        "SafetyProgram",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1)

                match resp with
                | DialogResult.Yes ->
                    use saveCommand = new SaveFile(kernelData)
                    (saveCommand :> ICommand).Execute(commandParam)
                    kernelData.Content <- None
                | DialogResult.No -> kernelData.Content <- None
                | DialogResult.Cancel -> ()
                | _ -> ()
            else
                ()

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish

    interface IDisposable with        
        member this.Dispose() = handler.Dispose()