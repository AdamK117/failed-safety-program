module SafetyProgram.Core.Models.Serialization.ChemicalTableXml

open CoshhChemicalXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open SafetyProgram.Base.FSharp.Helpers

let ChemicalTableXmlConverter = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise
        
    ConvertFrom = fun (data : XElement) ->
        maybeBuilder {
            let header = 
                getElement data "header"
                >>= getValue
                |> function
                    | Some(a) -> a
                    | None -> "Default Header"

            let! chemicals = 
                data.Elements(xname "coshhchemical")
                |> Seq.map(CoshhChemicalXmlConverter.ConvertFrom)
                |> flattenOptions

            return ChemicalTable({ Header=header; Chemicals=chemicals })
        }
}    
