module SafetyProgram.Core.Models.Serialization.HazardXml

open System.Xml.Linq
open System.Xml
open System
open SafetyProgram.Core.Models.FSerialization.Helpers
open SafetyProgram.Core.Models
open ConverterInterface

type HazardXml() = 
    interface IConverter<Hazard, XElement> with
        member this.ConvertTo(hazard : Hazard) = 
            raise (new NotImplementedException())

        member this.ConvertFrom(hazardXml : XElement) = 
            let warning = 
                match String.IsNullOrWhiteSpace(hazardXml.Value) |> not with
                | true -> Some(hazardXml.Value)
                | false -> None
            let signalWord = 
                match hazardXml.Attribute(xname "signalword") with
                | null -> ""
                | attr -> attr.Value
            let symbol = 
                match hazardXml.Attribute(xname "symbol") with
                | null -> ""
                | attr -> attr.Value

            if warning = None then
                None
            else
                Some <| { Warning=warning.Value; SignalWord=signalWord; Symbol=symbol; RiskPhrase=""; }
                

            