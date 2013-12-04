module SafetyProgram.Serialization.ChemicalTableXml

open CoshhChemicalXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open SafetyProgram.Base.Helpers
open FSharpx.Choice

let ChemicalTableXmlConverter : TwoWayConverter<ChemicalTable, XElement> = {
    ConvertTo = fun _ -> new NotImplementedException() |> raise        
    ConvertFrom = fun data -> choose {
        let header = 
            getElement "header" data >>= getValue
            |> function
                | Choice1Of2 x -> x
                | Choice2Of2 _ -> ""

        let! chemicals = 
            data.Elements(xname "coshhchemical")
            |> Seq.toList
            |> mapM CoshhChemicalXmlConverter.ConvertFrom

        return { Header=header; Chemicals=chemicals }
    }
}