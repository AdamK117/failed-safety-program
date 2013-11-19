module SafetyProgram.Core.Models.Serialization.CoshhChemicalXml

open ChemicalXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open SafetyProgram.Base.FSharp.Helpers

let CoshhChemicalXmlConverter = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) ->       
        maybeBuilder {
            let! amount = 
                maybeBuilder {
                    let! value = 
                        getElement data "amount" 
                        >>= getValue 
                        >>= parseDecimal

                    return!
                        getElement data "unit"
                        >>= getValue
                        >>= function
                            | "mL" -> Some <| (Millilitres <| value * 1m<mL>)
                            | "g" -> Some <| (Grammes <| value * 1m<g>)
                            | _ -> None
                }

            let! chemical = 
                getElement data "chemical" 
                >>= ChemicalXmlConverter.ConvertFrom

            return { Chemical = chemical; Quantity = amount; }
        }
}