namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System.Windows.Controls
open SafetyProgram.Base.Helpers
open System
open Fluent

type MainViewModel(ribbonVVM : GuiKernelData -> Ribbon * IViewModel, contentVVM : Option<GuiDocument> -> Control * IViewModel, model : GuiKernelData) =

    let propertyChanged = new Event<_,_>()

    let contentViewGen x =
        model.Content
        >>= fun y -> Some y.Content
        |> contentVVM

    let ribbonView, ribbonViewModel = ribbonVVM model
    let mutable contentView, contentViewModel = contentViewGen model.Content
    
    let handler = model.ContentChanged.Subscribe(fun newContent ->
        let x, y = contentViewGen newContent
        contentView<-x
        contentViewModel<-y)

    interface IMainViewModel with

        member this.RibbonView = ribbonView
        member this.ContentView = contentView

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Dispose () =
            ribbonViewModel.Dispose()
            contentViewModel.Dispose()
            handler.Dispose()

    // IMPLICIT IMPL
    member this.RibbonView = ribbonView
    member this.ContentView = contentView

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Dispose () =
        ribbonViewModel.Dispose()
        contentViewModel.Dispose()
        handler.Dispose()