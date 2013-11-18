namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Views.ModelViews.DocumentViews
open SafetyProgram.UI.Models.GuiModels
open SafetyProgram.Core.Models
open System.Windows.Controls
open ReactiveUI
open System

type DocumentViewModel(document : GuiDocument, contentGenerator : GuiChemicalTable -> Control) = 

    let propertyChanged = new Event<_,_>()
    let converter = Func<_,_>(contentGenerator)
    let documentObjects = document.Content.CreateDerivedCollection<_,_>(converter)
    
    interface IDocumentViewModel with
        member this.Width
            with get() = document.Format.Width / 0.1m<m>
        member this.Height
            with get() = document.Format.Height / 0.1m<m>
        member this.DocumentObjects
            with get() = documentObjects
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

