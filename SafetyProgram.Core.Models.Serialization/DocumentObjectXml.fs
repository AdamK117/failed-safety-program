module SafetyProgram.Core.Models.Serialization.DocumentObjectXml

open System.Xml.Linq
open ChemicalTableXml
open System
open SafetyProgram.Core.Models
open SafetyProgram.Base.Helpers
open FSharpx.Choice

let DocumentObjectXmlConverter : TwoWayConverter<DocumentObject, XElement> = {
    ConvertTo = fun _ -> new NotImplementedException() |> raise
    ConvertFrom = fun data -> 
        match data.Name.LocalName with
        | "chemicaltable" -> choose {
                let! x = ChemicalTableXmlConverter.ConvertFrom data
                return ChemicalTable x
            }         
        | _ -> Choice2Of2 "Unknown document object found."
}