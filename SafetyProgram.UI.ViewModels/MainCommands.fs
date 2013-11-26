namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Commands
open System

type MainCommands(model) = 

    member this.NewFile = new NewFile(model)
    member this.OpenFile = new OpenFile(model)
    member this.CloseFile = new CloseFile(model)
    member this.SaveFile = new SaveFile(model)
    member this.SaveAs = new SaveAs(model)

    interface IDisposable with
        member this.Dispose() =
            ()