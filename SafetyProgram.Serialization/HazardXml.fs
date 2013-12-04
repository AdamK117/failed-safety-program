module SafetyProgram.Serialization.HazardXml

open SafetyProgram.Base.Helpers
open System.Xml.Linq
open System
open SafetyProgram.Core.Models
open FSharpx.Choice

// Converts hazard records to and from an XML format.
let HazardXmlConverter : TwoWayConverter<Hazard, XElement> =  {
    ConvertTo = fun _ -> NotImplementedException() |> raise
    ConvertFrom = fun data -> choose {
        let! warning = 
            let x = data.Value
            match String.IsNullOrWhiteSpace x with
            | false -> Choice1Of2 x
            | true -> Choice2Of2 "No warning found for a hazard tag. A hazard tag must have a warning."            

        let signalWord = 
            getAttribute "signalword" data
            >>= getAttributeValue
            |> function
                | Choice1Of2 x -> x
                | Choice2Of2 _ -> ""

        let symbol = 
            getAttribute "symbol" data
            >>= getAttributeValue
            |> function
                | Choice1Of2 x -> x
                | Choice2Of2 _ -> ""

        return { Warning = warning; SignalWord = signalWord; Symbol = symbol; RiskPhrase=""; }
    }
}