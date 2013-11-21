namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models

type InsertRibbonTabViewModel(model : GuiDocument) = 

    let propertyChanged = new Event<_,_>()

    interface IInsertRibbonTabViewModel with

        member this.Filler = "NYI"

        member this.Dispose() = ()

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    // IMPLICIT IMPL
    member this.Filler = "NYI"

    member this.Dispose() = ()

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish