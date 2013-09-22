module SafetyProgram.Core.Models.Serialization.ChemicalXml

open HazardXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open Core

let ChemicalXmlConverter = {        
    ConvertTo = fun data ->
        new NotImplementedException() |> raise
        
    ConvertFrom = fun (data : XElement) ->
        maybeBuilder {
            let! name = 
                getElement data "name"
                >>= getValue

            let! hazards = 
                maybeBuilder {
                    let! hazardsElement = getElement data "hazards"
                    return!                            
                        hazardsElement.Elements(xname "hazard")
                        |> Seq.map(HazardXmlConverter.ConvertFrom)
                        |> flattenOptions
                }

            return { Name = name; Hazards = hazards; }
        }     
}
                        

