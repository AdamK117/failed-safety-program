namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Views.MainViews

type MainViewModel(ribbonView, contentView) =
    
    interface IMainViewModel with
        member this.RibbonView = ribbonView
        member this.ContentView = contentView

