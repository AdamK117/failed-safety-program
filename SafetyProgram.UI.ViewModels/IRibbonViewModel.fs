namespace SafetyProgram.UI.ViewModels

open Fluent
open System.Collections.ObjectModel

type IRibbonViewModel = 
    abstract RibbonTabs : ObservableCollection<RibbonTabItem> with get



