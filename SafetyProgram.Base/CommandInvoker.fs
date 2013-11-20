namespace SafetyProgram.Base

open System.Collections.Generic

type ReversibleCommand = {
    Execute : unit -> unit
    UnExecute : unit -> unit
}

type ICommandInvoker = 
    abstract InvokeCommand : ReversibleCommand -> unit

type ICommandController =
    
    inherit ICommandInvoker

    abstract Undo : unit -> unit
    abstract CanUndo : unit -> bool
    abstract CanUndoChanged : IEvent<bool>

    abstract Redo : unit -> unit
    abstract CanRedo : unit -> bool
    abstract CanRedoChanged : IEvent<bool>

type CommandController () =

    let canUndoChanged = new Event<_>()
    let canRedoChanged = new Event<_>()

    let futureCommands = new Stack<_>()
    let pastCommands = new Stack<_>()

    let canUndo() = pastCommands.Count > 0
    let canRedo() = futureCommands.Count > 0

    let invokeCommand f = 
        let canUndoBefore = canUndo()
        let canRedoBefore = canRedo()

        f()

        let canUndoAfter = canUndo()
        if canUndoAfter <> canUndoBefore then
            canUndoChanged.Trigger canUndoAfter

        let canRedoAfter = canRedo()
        if canRedoAfter <> canRedoBefore then
            canRedoChanged.Trigger canRedoAfter

    interface ICommandController with
        member this.InvokeCommand (cmd) =

            let f() = 
                futureCommands.Clear()
                cmd.Execute()
                pastCommands.Push cmd

            invokeCommand f

        member this.Undo() = 
            
            let f() = 
                let cmd = pastCommands.Pop ()
                cmd.UnExecute ()
                futureCommands.Push cmd

            invokeCommand f

        member this.CanUndo() = canUndo()

        member this.CanUndoChanged = canUndoChanged.Publish

        member this.Redo() = 
            
            let f() = 
                let cmd = futureCommands.Pop()
                cmd.Execute()
                pastCommands.Push cmd

            invokeCommand f

        member this.CanRedo() = canRedo()

        member this.CanRedoChanged = canRedoChanged.Publish

