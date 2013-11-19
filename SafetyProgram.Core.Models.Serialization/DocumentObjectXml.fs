module SafetyProgram.Core.Models.Serialization.DocumentObjectXml

open System.Xml.Linq
open ChemicalTableXml
open System
open SafetyProgram.Core.Models
open SafetyProgram.Base.FSharp.Helpers

let DocumentObjectXmlConverter = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) ->
        match data.Name.LocalName with
        | "chemicaltable" -> ChemicalTableXmlConverter.ConvertFrom data
        | _ -> None
}