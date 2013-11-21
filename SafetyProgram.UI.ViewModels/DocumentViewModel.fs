namespace SafetyProgram.UI.ViewModels

open SafetyProgram.UI.Models
open SafetyProgram.Core.Models
open System.Windows.Controls
open ReactiveUI
open System

type DocumentViewModel(contentGenerator : GuiDocumentObject -> Control * IViewModel, model : GuiDocument) = 

    let propertyChanged = new Event<_,_>()

    let docObjectPairs = model.Content.CreateDerivedCollection<_,_>(Func<_,_> contentGenerator)
    let docObjectViews = docObjectPairs.CreateDerivedCollection<_,_>(Func<_,_> fst)
    
    interface IDocumentViewModel with
        member this.Width
            with get () = model.Format.Width / 0.1m<m>

        member this.Height
            with get () = model.Format.Height / 0.1m<m>

        member this.DocumentObjects
            with get () = docObjectViews

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Dispose() = 

            // Dispose all child viewmodels.
            docObjectPairs
            |> Seq.iter (fun (_, vm) -> vm.Dispose())

            // Dispose reactive collections linked into guimodel collection.
            docObjectViews.Dispose()
            docObjectPairs.Dispose()

    // IMPLICIT IMPL
    member this.Width
        with get () = model.Format.Width / 0.1m<m>

    member this.Height
        with get () = model.Format.Height / 0.1m<m>

    member this.DocumentObjects
        with get () = docObjectViews

    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Dispose() = 

        // Dispose all child viewmodels.
        docObjectPairs
        |> Seq.iter (fun (_, vm) -> vm.Dispose())

        // Dispose reactive collections linked into guimodel collection.
        docObjectViews.Dispose()
        docObjectPairs.Dispose()