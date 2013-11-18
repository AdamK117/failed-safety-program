module SafetyProgram.Core.Models.Serialization.HazardXml

open Core
open System.Xml.Linq
open System
open SafetyProgram.Core.Models

// Converts hazard records to and from an XML format.
let HazardXmlConverter =  {
    ConvertTo = fun data ->
        NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) -> maybeBuilder {
        let! warning = 
            let warningContents = data.Value
            match warningContents |> String.IsNullOrWhiteSpace with
            | true -> None
            | false -> Some(warningContents)

        let signalWord = 
            getAttribute data "signalword"
            >>= getAttributeValue
            |> function
                | Some(a) -> a
                | None -> ""

        let symbol = 
            getAttribute data "symbol"
            >>= getAttributeValue
            |> function
                | Some(a) -> a
                | None -> ""

        return { Warning = warning; SignalWord = signalWord; Symbol = symbol; RiskPhrase=""; }
    }
}