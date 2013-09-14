namespace SafetyProgram.UI.ViewModels

open System
open System.ComponentModel
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews

type RibbonViewModel(model, configuration, commandInvoker, selectionManager,documentRibbonFactory) =
    let ribbonTabs = new ObservableCollection<RibbonTabItem>()
    let commands = new CoreCommands(model, commandInvoker)

    // Handler for document changes
    let documentChanged newDoc = 
        ribbonTabs.Clear()
        if (newDoc <> null) then
            documentRibbonFactory(newDoc)
            |> List.iter(fun tab ->
                ribbonTabs.Add(tab))
    
    // Add handler, update if necessary.
    do
        model.DocumentChanged.Add(fun e->
            documentChanged(e.NewProperty))
        documentChanged(model.Document)            

    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabs
        member this.Commands = commands :> ICoreCommands

    member this.RibbonTabs = ribbonTabs
    member this.Commands = commands :> ICoreCommands