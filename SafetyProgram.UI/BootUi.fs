namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.Core.Kernel
open SafetyProgram.UI.Views
open SafetyProgram.UI.ViewModels

type MainUiController(svc) = 
    let currentData = Async.RunSynchronously <| svc.Current()

    let ribbonViewModel = new RibbonViewModel(null) // Need OC for ribbon tab items
    let ribbonView = new DefaultRibbonView(ribbonViewModel)

    let mainViewModel = new MainViewModel(ribbonView, null) // Need content view (changes with document?)
    let mainView = new DefaultMainView(mainViewModel)

    member x.View = (mainView :> Window)