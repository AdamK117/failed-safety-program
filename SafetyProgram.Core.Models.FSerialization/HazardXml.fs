module HazardXml

open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

type HazardXml() = 
    interface IConverter<Hazard, XElement> with
        member this.ConvertTo(hazard : Hazard) = 
            raise (new NotImplementedException())

        member this.ConvertFrom(hazardXml : XElement) = 
            let mutable errorFlag = false

            let warning = 
                match String.IsNullOrWhiteSpace(hazardXml.Value) |> not with
                    | true -> hazardXml.Value
                    | false -> errorFlag<-true
            let signalWord = 
                hazardXml.Attribute("signalword") |> function
                    | null -> ""
                    | attr -> attr.Value
            let symbol = 
                hazardXml.Attribute("symbol") |> function
                    | null -> ""
                    | attr -> attr.Value

            match errorFlag with
                | true -> None
                | false -> 
                    Some({ Warning=warning; SignalWord=signalWord; Symbol=symbol; RiskPhrase=""; })

            