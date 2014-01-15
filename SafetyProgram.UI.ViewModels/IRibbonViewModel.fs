namespace SafetyProgram.UI.ViewModels

open Fluent
open System.Collections.ObjectModel

type IRibbonViewModel = 

    inherit IViewModel

    abstract RibbonTabs : ObservableCollection<RibbonTabItem> with get
    abstract SelectionRibbonTabs : ObservableCollection<RibbonTabItem> with get
    abstract Commands : MainCommands with get