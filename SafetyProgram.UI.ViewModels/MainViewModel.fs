namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Windows.Controls
open SafetyProgram.Base.Helpers
open System
open Fluent
open FSharpx.Option

type MainViewModel(ribbonVVM : GuiKernelData -> Ribbon * IViewModel, contentVVM : Option<GuiDocument> -> Control * IViewModel, model : GuiKernelData) as this =

    let propertyChanged = new Event<_,_>()

    let contentViewGen x =
        model.Content
        >>= fun y -> Some y.Content
        |> contentVVM

    let ribbonView, ribbonViewModel = ribbonVVM model
    let mutable contentView, contentViewModel = contentViewGen model.Content

    let commands = new MainCommands(model)
    
    let handler = model.ContentChanged.Subscribe(fun newContent ->
        let x, y = contentViewGen newContent
        contentView<-x
        contentViewModel<-y
        raisePropChanged propertyChanged this "ContentView")

    interface IMainViewModel with

        member this.RibbonView = ribbonView
        member this.ContentView = contentView

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Dispose () =
            ribbonViewModel.Dispose()
            contentViewModel.Dispose()
            handler.Dispose()
            (commands :> IDisposable).Dispose()

        member this.Commands = commands

    // IMPLICIT IMPL
    member this.RibbonView = ribbonView
    member this.ContentView = contentView

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Dispose () =
        ribbonViewModel.Dispose()
        contentViewModel.Dispose()
        handler.Dispose()
        (commands :> IDisposable).Dispose()

    member this.Commands = commands