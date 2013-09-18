module SafetyProgram.Core.Models.FSerialization.ChemicalXml

open HazardXml
open ConverterInterface
open Helpers
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

type ChemicalXml() = 
    interface IConverter<Chemical, XElement> with
        member this.ConvertTo(chemical : Chemical) = 
            raise (new NotImplementedException())

        member this.ConvertFrom(chemicalXml : XElement) = 
            // Required: Get the chemicals name.
            let name = 
                match chemicalXml.Element(xname "name") with
                    | null -> None
                    | nameElement -> Some(nameElement.Value)
            
            // Optional: Get the hazards associated with the chemical.
            let hazards = 
                // 'Hazards' tag contains 'hazard' elements
                let hazardsElement = chemicalXml.Element(xname "hazards")
                // Check it exists. If it doesn't, it's not required.
                match hazardsElement with
                    | null -> Some(Seq.empty)
                    | _ ->
                        // Get a hazard converter. Pump hazards through it.
                        let hazardConverter = HazardXml() :> IConverter<Hazard, XElement>
                        hazardsElement.Elements(xname "hazard")
                        |> Seq.map(hazardConverter.ConvertFrom)
                        |> fun hazards ->
                            // If the converter returned a 'None' (invalid data)
                            // propagate it upto this parse.
                            match hazards |> Seq.exists(fun item -> item = None) with
                                | true -> None
                                | false -> hazards |> Seq.map(fun hazard -> hazard.Value) |> Some
            
            // If theres errors, return 'None'; otherwise, return the converted product.
            if (name = None) || (hazards = None) then 
                None
            else 
                Some <| { Name=name.Value; Hazards=hazards.Value; }
                        

