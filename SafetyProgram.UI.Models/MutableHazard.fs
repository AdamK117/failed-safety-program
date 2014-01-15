namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiHazard(hazard) = 

    let propertyChangedEvent = new Event<_,_>() 

    let mutable warning = hazard.Warning
    let warningChanged = Event<_>()

    let mutable riskPhrase = hazard.RiskPhrase
    let riskPhraseChanged = Event<_>()

    let mutable signalWord = hazard.SignalWord
    let signalWordChanged = Event<_>()

    let mutable symbol = hazard.Symbol
    let symbolChanged = Event<_>()

    let mutable selected = false
    let selectedChanged = Event<_>()

    member this.Warning
        with get () = warning
        and set x = 
            warning <- x
            raisePropChanged propertyChangedEvent this "Warning"
            warningChanged.Trigger <| warning

    member this.WarningChanged = warningChanged.Publish
    
    member this.SignalWord
        with get () = signalWord
        and set x = 
            signalWord <- x
            raisePropChanged propertyChangedEvent this "SignalWord"
            signalWordChanged.Trigger <| signalWord

    member this.SignalWordChanged = signalWordChanged.Publish
    
    member this.Symbol
        with get () = symbol
        and set x = 
            symbol <- x
            raisePropChanged propertyChangedEvent this "Symbol"
            symbolChanged.Trigger <| symbol

    member this.SymbolChanged = symbolChanged.Publish
    
    member this.RiskPhrase
        with get () = riskPhrase
        and set x = 
            riskPhrase <- x
            raisePropChanged propertyChangedEvent this "RiskPhrase"
            riskPhraseChanged.Trigger riskPhrase

    member this.RiskPhraseChanged = riskPhraseChanged.Publish

    member this.Selected 
        with get () = selected
        and set x = 
            selected <- x

    member this.SelectedChanged = selectedChanged.Publish

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module HazardHelpers = 

    let guiHazardToModel (guiHazard : GuiHazard) = {
        Warning = guiHazard.Warning
        SignalWord = guiHazard.SignalWord
        Symbol = guiHazard.Symbol
        RiskPhrase = guiHazard.RiskPhrase
    }