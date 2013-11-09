module KernelCommands

/// Defines commands that act on the general kernel data. 
/// e.g changing documents, changing IO, changing config, etc.

open System
open SafetyProgram.Core.CommandCore
open SafetyProgram.Core
open SafetyProgram.Core.IO.Services
open SafetyProgram.Core.Models
open SafetyProgram.Core
open System.Xml.Linq

let closeDocument = {
    CanExecute = fun a ->
        match a.Content with
        | Some(b) -> true
        | None -> false
    Execute = fun a ->
        { a with Content = None }
}

let newDocument = {
    CanExecute = fun _ ->
        true
    Execute = fun (a : KernelData) ->
        { a with Content = None }
}

let openDocument = {
    CanExecute = fun _ ->
        true
    Execute = fun (a : KernelData) ->
        match a.Service with
        | Local(b) -> 
            let loadedFile = b.Load "C:\Data" |> Async.RunSynchronously
            { a with Content = loadedFile }
}

let redo = {
    CanExecute = fun _ ->
        // Need additional information (e.g. a state stack)
    Execute = fun a ->
        // Need additional information (e.g. an a->a transform or a 'new' a)
}

let undo a = {
    CanExecute = fun _ ->
        // Need additional information (e.g. a state / transform stack).
    Execute = fun a ->
        // Need additional information (e.g. a state / transform stack).
}

let save = {
    CanExecute = fun _ -> 
        true
    Execute = fun (a : KernelData) ->
        match a.Service with
        | Local(b) -> // Need additional information and a more informative return type (e.g. a->a isn't adequate for failure, need a->Option a)
}

let saveAs = {
    CanExecute = fun _ ->
        true
    Execute = fun (a : KernelData) ->
        match a.Service with
        | Local(b) -> // Need additional information and a more infromative return type (e.g. an a->a isn't adequate for IO fails).
}
