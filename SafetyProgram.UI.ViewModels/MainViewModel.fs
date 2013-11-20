namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Windows.Controls
open System

type MainViewModel(ribbonView, kernelData : GuiKernelData, contentViewGenerator : GuiDocument->Control) =

    let contentView kData = 
        match kernelData.Content with
        | Some (doc, _) -> contentViewGenerator doc
        | None -> null

    interface IMainViewModel with
        member this.RibbonView = ribbonView
        member this.ContentView = contentView kernelData

    interface IDisposable with
        member this.Dispose () =
            ()