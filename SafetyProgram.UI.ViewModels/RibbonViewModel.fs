namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Base.Helpers
open FSharpx.Option
open System

type RibbonViewModel(documentTabGenerator : Option<GuiDocument> -> ObservableCollection<RibbonTabItem>, selectionTabGenerator : GuiDocumentObject -> RibbonTabItem, model : GuiKernelData) as this = 
    
    let propertyChanged = new Event<_,_>()

    let commands = new MainCommands(model)

    let a = None >>= (fun a -> Some a)

    let getTabs content =
        model.Content
        >>= (fun c -> Some c.Content)
        |> documentTabGenerator

    let mutable ribbonTabs = getTabs model.Content

    let handler = model.ContentChanged.Subscribe(fun _ ->
        ribbonTabs <- getTabs model.Content
        raisePropChanged propertyChanged this "RibbonTabs")

    interface IRibbonViewModel with

        member this.RibbonTabs = ribbonTabs

        member this.Dispose() = 
            (commands :> IDisposable).Dispose()
            handler.Dispose()

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Commands = commands

    // IMPLICIT IMPL (COPY FROM IRibbonViewModel)
    member this.RibbonTabs = ribbonTabs

    member this.Dispose() = 
        (commands :> IDisposable).Dispose()
        handler.Dispose()

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Commands = commands