namespace SafetyProgram.UI.ViewModels

type RibbonViewModel(ribbonTabs) = 
    
    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabs
    