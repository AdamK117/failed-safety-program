namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Models
open System

type UiController(model : GuiKernelData) = 

    let ribbonViewModel = new RibbonViewModel(null) // Need OC for ribbon tab items
    let ribbonView = new DefaultRibbonView(ribbonViewModel)

    let mainViewModel = new MainViewModel(ribbonView, model, null) // Need content view (changes with document?)
    let mainView = new DefaultMainView(mainViewModel)

    member x.View = (mainView :> Window)

    interface IDisposable with
        member this.Dispose() =
            ()