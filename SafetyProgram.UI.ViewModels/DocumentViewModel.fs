namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open SafetyProgram.Core.Models
open System.Windows.Controls
open ReactiveUI
open System

type DocumentViewModel(document, contentGenerator) = 

    let propertyChanged = new Event<_,_>()
    let converter = Func<_,_>(contentGenerator)
    let documentObjects = document.Content.CreateDerivedCollection<_,_>(converter)
    
    interface IDocumentViewModel with
        member this.Width
            with get () = document.Format.Width / 0.1m<m>

        member this.Height
            with get () = document.Format.Height / 0.1m<m>

        member this.DocumentObjects
            with get () = documentObjects

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

