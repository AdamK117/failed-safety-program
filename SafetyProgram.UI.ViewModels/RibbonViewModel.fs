namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews

type RibbonViewModel(model : Option<Document>, provider : IEvent<Document>, tabFactory : Document->seq<RibbonTabItem>) as this = 
    let mutable ribbonTabs =
        match model with
        | Some model -> model |> tabFactory
        | None -> null

    let propertyChangedEvent = new Event<_,_>()
    
    do
        provider.Add(fun newDocument ->
                        ribbonTabs <- newDocument |> tabFactory
                        propertyChangedEvent.Trigger(
                            this,
                            new PropertyChangedEventArgs("RibbonTabs")))

    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabs
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

    member this.RibbonTabs = ribbonTabs
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish