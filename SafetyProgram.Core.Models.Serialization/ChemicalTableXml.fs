module SafetyProgram.Core.Models.Serialization.ChemicalTableXml

open Helpers
open CoshhChemicalXml
open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

let ChemicalTableXmlConverter = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise
        
    ConvertFrom = fun (data : XElement) ->
        let header = 
            match data.Element(xname "header") with
            | null -> Some <| "Default Header"
            | headerElement -> Some <| headerElement.Value

        let chemicals = 
            let chemicalConverter = CoshhChemicalXmlConverter
            data.Elements(xname "coshhchemical")
            |> Seq.map(chemicalConverter.ConvertFrom)
            |> fun coshhChemicals ->
                match coshhChemicals |> Seq.exists(fun coshhChemical -> coshhChemical = None) with
                | true -> None
                | false -> coshhChemicals |> Seq.map(fun coshhChemical -> coshhChemical.Value) |> Some

        if (header <> None) || (chemicals <> None) then
            Some <| { Header=header.Value; Chemicals=chemicals.Value; }
        else
            None
}    
