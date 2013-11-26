﻿namespace SafetyProgram.Base

open System.Collections.Generic

type ReversibleCommand = {
    Execute : unit -> unit
    UnExecute : unit -> unit
}

type ICommandInvoker = 
    abstract invoke : ReversibleCommand -> unit

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

    // Hold commands
    let futureCommands = new Stack<_>()
    let pastCommands = new Stack<_>()

    // Predicates for executing a past or future command.
    let canUndo() = pastCommands.Count > 0
    let canRedo() = futureCommands.Count > 0

    // Invoke a function (not a command)
    let invoke f = 
        let canUndoBefore = canUndo()
        let canRedoBefore = canRedo()

        f()

        // Echo to public event that a change in state has occured.
        let canUndoAfter = canUndo()
        if canUndoAfter <> canUndoBefore then
            canUndoChanged.Trigger canUndoAfter

        // Same as above.
        let canRedoAfter = canRedo()
        if canRedoAfter <> canRedoBefore then
            canRedoChanged.Trigger canRedoAfter

    // Outer interface declaration.
    interface ICommandController with
        // Invoke a command.
        member this.invoke (cmd) =

            let f() = 
                futureCommands.Clear()
                cmd.Execute()
                pastCommands.Push cmd

            invoke f

        // Undo the last invoked command.
        member this.Undo() = 
            
            let f() = 
                let cmd = pastCommands.Pop ()
                cmd.UnExecute ()
                futureCommands.Push cmd

            invoke f

        member this.CanUndo() = canUndo()

        member this.CanUndoChanged = canUndoChanged.Publish

        // Redo the last undone command.
        member this.Redo() = 
            
            let f() = 
                let cmd = futureCommands.Pop()
                cmd.Execute()
                pastCommands.Push cmd

            invoke f

        member this.CanRedo() = canRedo()

        member this.CanRedoChanged = canRedoChanged.Publish