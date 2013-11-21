namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiFormat(format) =

    let propertyChangedEvent = new Event<_,_>()

    let mutable width = format.Width
    let widthChanged = new Event<_>()

    let mutable height = format.Height
    let heightChanged = new Event<_>()

    member this.Width
        with get () = width
        and set x = 
            width <- x
            raisePropChanged propertyChangedEvent this "Width"
            widthChanged.Trigger width

    member this.WidthChanged = widthChanged.Publish

    member this.Height
        with get () = height
        and set x = 
            height <- x
            raisePropChanged propertyChangedEvent this "Height"
            heightChanged.Trigger height

    member this.HeightChanged = heightChanged.Publish

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module FormatHelpers =

    let guiFormatToFormat (guiFormat : GuiFormat) = {
        Width = guiFormat.Width
        Height = guiFormat.Height
    }