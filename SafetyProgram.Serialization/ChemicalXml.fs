module SafetyProgram.Serialization.ChemicalXml

open HazardXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open SafetyProgram.Base.Helpers
open FSharpx.Choice

let ChemicalXmlConverter : TwoWayConverter<Chemical, XElement> = {        
    ConvertTo = fun _ -> new NotImplementedException() |> raise        
    ConvertFrom = fun data -> choose {
        let! name = getElement "name" data >>= getValue

        let! hazards = choose {
            let! x = getElement "hazards" data
            let hazards =
                x.Elements(xname "hazard")
                |> Seq.toList 
                |> mapM HazardXmlConverter.ConvertFrom
            return! hazards
        }

        return { Name = name; Hazards = hazards; }
    }     
}