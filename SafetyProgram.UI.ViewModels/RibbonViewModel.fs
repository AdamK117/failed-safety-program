namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open Fluent
open System.Collections.ObjectModel
open SafetyProgram.Base.Helpers
open System

type RibbonViewModel(documentTabGenerator : Option<GuiDocument> -> ObservableCollection<RibbonTabItem>, selectionTabGenerator : GuiDocumentObject -> RibbonTabItem, model : GuiKernelData) = 
    
    let propertyChanged = new Event<_,_>()

    interface IRibbonViewModel with

        member this.RibbonTabs = 
            model.Content
            >>= (fun c -> Some c.Content)
            |> documentTabGenerator

        member this.Dispose() = ()

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    // IMPLICIT IMPL
    member this.RibbonTabs = 
            model.Content
            >>= (fun c -> Some c.Content)
            |> documentTabGenerator

    member this.Dispose() = ()

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish