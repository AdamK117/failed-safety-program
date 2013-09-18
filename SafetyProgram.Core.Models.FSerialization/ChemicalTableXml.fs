module SafetyProgram.Core.Models.FSerialization.ChemicalTableXml

open Helpers
open CoshhChemicalXml
open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

type ChemicalTableXml() = 

    interface IConverter<ChemicalTable, XElement> with

        // Serialize a coshh chemical into an XML format.
        member this.ConvertTo(data : ChemicalTable) = 
            raise (new NotImplementedException())

        // Deserialize a coshh chemical stored in an XML format.
        member this.ConvertFrom(data : XElement) = 
            let header = 
                match data.Element(xname "header") with
                | null -> Some <| "Default Header"
                | headerElement -> Some <| headerElement.Value

            let chemicals = 
                let chemicalConverter = new CoshhChemicalXml() :> IConverter<CoshhChemical, XElement>
                data.Elements(xname "coshhchemical")
                |> Seq.map(chemicalConverter.ConvertFrom)
                |> fun coshhChemicals ->
                    match coshhChemicals |> Seq.exists(fun coshhChemical -> coshhChemical = None) with
                    | true -> None
                    | false -> coshhChemicals |> Seq.map(fun coshhChemical -> coshhChemical.Value) |> Some

            if (header <> None) || (chemicals <> None) then
                Some <| ChemicalTable { Header=header.Value; Chemicals=chemicals.Value; }
            else
                None
