namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Views.MainViews

type RibbonViewModel(ribbonTabs) = 
    
    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabs
    