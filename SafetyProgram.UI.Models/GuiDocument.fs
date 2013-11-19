namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.FSharp.Helpers

type GuiDocument (document) = 

    let propertyChangedEvent = new Event<_,_>()

    let content = 
        let docObjs = 
            document.Content
            |> Seq.map DocumentObjectHelpers.guiDocumentObjectFactory
        new ObservableCollection<_>(docObjs)

    let mutable format = document.Format
    let formatChanged = new Event<_>()

    member this.Content = content

    member this.Format
        with get () = format
        and set x = 
            format <- x
            raisePropChanged propertyChangedEvent this "Format"
            formatChanged.Trigger format

    member this.FormatChanged = formatChanged.Publish

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module DocumentHelpers = 

    let guiDocumentToDocument (guiDocument : GuiDocument) = {
        Content = Seq.map DocumentObjectHelpers.guiDocumentObjectToDocumentObject guiDocument.Content
        Format = guiDocument.Format
    }