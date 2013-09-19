module SafetyProgram.Core.Models.Serialization.HazardXml

open System.Xml.Linq
open System.Xml
open System
open Helpers
open SafetyProgram.Core.Models
open ConverterInterface

let HazardXmlConverter =  {
    ConvertTo = fun data ->
        NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement)->
        let warning = 
            match String.IsNullOrWhiteSpace(data.Value) |> not with
            | true -> Some(data.Value)
            | false -> None
        let signalWord = 
            match data.Attribute(xname "signalword") with
            | null -> ""
            | attr -> attr.Value
        let symbol = 
            match data.Attribute(xname "symbol") with
            | null -> ""
            | attr -> attr.Value

        if warning = None then
            None
        else
            Some <| { Warning=warning.Value; SignalWord=signalWord; Symbol=symbol; RiskPhrase=""; }
}