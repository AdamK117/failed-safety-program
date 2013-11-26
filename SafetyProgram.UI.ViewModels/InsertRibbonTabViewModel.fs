namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open System

type InsertRibbonTabViewModel(model : GuiDocument) = 

    let propertyChanged = new Event<_,_>()

    let commands = new InsertTabCommands(model)

    interface IInsertRibbonTabViewModel with
        member this.Filler = "NYI"
        member this.Dispose() = (commands :> IDisposable).Dispose()
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish
        member this.Commands = commands

    // IMPLICIT IMPL
    member this.Filler = "NYI"
    member this.Dispose() = (commands :> IDisposable).Dispose()
    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish
    member this.Commands = commands