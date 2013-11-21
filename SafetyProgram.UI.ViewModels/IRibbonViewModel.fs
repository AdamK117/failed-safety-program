namespace SafetyProgram.UI.ViewModels

open Fluent
open System.Collections.ObjectModel

type IRibbonViewModel = 

    inherit IViewModel

    abstract RibbonTabs : ObservableCollection<RibbonTabItem> with get