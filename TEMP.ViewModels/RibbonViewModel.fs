namespace TEMP.ViewModels

open System
open System.ComponentModel
open SafetyProgram.UI.Views.MainView.Default
open SafetyProgram.Core
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Core.Commands.ICommands
open SafetyProgram.Core.Models

type RibbonViewModel(model, configuration, commandInvoker, selectionManager,documentRibbonFactory) as this =
    let ribbonTabs = new ObservableCollection<RibbonTabItem>()
    let commands = new CoreCommands(model, commandInvoker)

    // Handler for document changes
    let documentChanged newDoc = 
        ribbonTabs.Clear()
        if (newDoc <> null) then
            documentRibbonFactory(newDoc)
            |> Seq.iter(fun tab ->
                ribbonTabs.Add(tab))
    
    // Add handler, update if necessary.
    do
        model.DocumentChanged.Add(fun e->
            documentChanged(e.NewProperty))
        documentChanged(model.Document)            

    interface IRibbonViewModel with
        member this.RibbonTabs = ribbonTabs
        member this.Commands = commands :> ICoreCommands