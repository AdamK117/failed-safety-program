module SafetyProgram.UI.Models.GuiModels

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.FSharp.Helpers

#if INTERACTIVE

#r @"C:\Users\Adam\Desktop\SafetyProgram\SafetyProgram.Core.Models\bin\Debug\SafetyProgram.Core.Models.dll"

#endif

type GuiHazard(hazard) = 

    let propertyChangedEvent = new Event<_,_>() 

    let mutable warning = hazard.Warning
    let mutable riskPhrase = hazard.RiskPhrase
    let mutable signalWord = hazard.SignalWord
    let mutable symbol = hazard.Symbol

    member this.Warning
        with get () = warning
        and set parameter = 
            warning<-parameter
            raisePropChanged propertyChangedEvent this "Warning"
    
    member this.SignalWord
        with get () = signalWord
        and set parameter = 
            signalWord<-parameter
            raisePropChanged propertyChangedEvent this "SignalWord"
    
    member this.Symbol
        with get () = symbol
        and set parameter = 
            symbol<-parameter
            raisePropChanged propertyChangedEvent this "SignalWord"
    
    member this.RiskPhrase
        with get () = riskPhrase
        and set parameter = 
            riskPhrase<-parameter
            raisePropChanged propertyChangedEvent this "SignalWord"

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

let guiHazardToModel (guiHazard : GuiHazard) = {
    Warning = guiHazard.Warning
    SignalWord = guiHazard.SignalWord
    Symbol = guiHazard.Symbol
    RiskPhrase = guiHazard.RiskPhrase
}

type GuiChemical(chemical) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable name = chemical.Name
    let hazards = 
        new ObservableCollection<_>(
            chemical.Hazards
            |> Seq.map (fun hazard -> 
                new GuiHazard(hazard)))

    member this.Name
        with get () = name
        and set parameter = 
            name<-parameter
            raisePropChanged propertyChangedEvent this "Name"

    member this.Hazards
        with get () = hazards

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

let guiChemicalToChemical (guiChemical : GuiChemical) = {
    Name = guiChemical.Name
    Hazards = Seq.map guiHazardToModel guiChemical.Hazards    
}

type GuiCoshhChemical(coshhChemical) = 

    let propertyChangedEvent = new Event<_,_>()
    
    let mutable chemical = new GuiChemical(coshhChemical.Chemical)
    let mutable quantity = coshhChemical.Quantity

    member this.Chemical
        with get () = chemical
        and set param = 
            chemical<-param
            raisePropChanged propertyChangedEvent this "Chemical"

    member this.Quantity
        with get() = quantity
        and set param = 
            quantity<-param
            raisePropChanged propertyChangedEvent this "Quantity"

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

let guiCoshhChemicalToCoshhChemical (guiCoshhChemical : GuiCoshhChemical) = {
    Chemical = guiChemicalToChemical guiCoshhChemical.Chemical
    Quantity = guiCoshhChemical.Quantity
}

type GuiFormat(format) =

    let propertyChangedEvent = new Event<_,_>()

    let mutable width = format.Width
    let mutable height = format.Height

    member this.Width
        with get() = width
        and set x = 
            width<-x
            raisePropChanged propertyChangedEvent this "Width"

    member this.Height
        with get() = height
        and set x = 
            height<-x
            raisePropChanged propertyChangedEvent this "Height"

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

let guiFormatToFormat (guiFormat : GuiFormat) = {
    Width = guiFormat.Width
    Height = guiFormat.Height
}

type GuiChemicalTable(chemicalTable) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable header = chemicalTable.Header

    let chemicals = 
        new ObservableCollection<_>(
            chemicalTable.Chemicals
            |> Seq.map (fun chemical -> new GuiCoshhChemical(chemical)))

    member this.Header
        with get () = header
        and set x = 
            header<-x
            raisePropChanged propertyChangedEvent this "Header"

    member this.Chemicals
        with get () = chemicals

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

let guiChemicalTableToChemicalTable (guiChemicalTable : GuiChemicalTable) = {
    Header = guiChemicalTable.Header
    Chemicals = Seq.map guiCoshhChemicalToCoshhChemical guiChemicalTable.Chemicals
}

type GuiDocumentObject = 
    | GuiChemicalTable of GuiChemicalTable

let guiDocumentObjectFactory docObject = 
    match docObject with
    | ChemicalTable(x) -> new GuiChemicalTable(x)

type GuiDocument (document) = 

    let propertyChangedEvent = new Event<_,_>()

    let content = 
        let docObjs = 
            document.Content
            |> Seq.map (guiDocumentObjectFactory)
        new ObservableCollection<_>(docObjs)

    let mutable format = document.Format

    member this.Content = content

    member this.Format
        with get () = format
        and set x = 
            format<-x
            raisePropChanged propertyChangedEvent this "Header"

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish