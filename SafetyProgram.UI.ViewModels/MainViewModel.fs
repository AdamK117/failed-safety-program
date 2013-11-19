namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Views
open SafetyProgram.UI.Models
open System.Windows.Controls
open System

type MainViewModel(ribbonView, kernelData : GuiKernelData, contentViewGenerator : GuiDocument->Control) =

    interface IMainViewModel with
        member this.RibbonView = ribbonView
        member this.ContentView = contentViewGenerator kernelData.Content

    interface IDisposable with
        member this.Dispose () =
            ()