module SafetyProgram.Serialization.CoshhChemicalXml

open ChemicalXml
open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open SafetyProgram.Base.Helpers
open FSharpx.Choice

let CoshhChemicalXmlConverter : TwoWayConverter<CoshhChemical, XElement> = {
    ConvertTo = fun _ -> new NotImplementedException() |> raise
    ConvertFrom = fun data -> choose {
        let! amount = choose {
            let! value = getElement "amount"  data >>= getValue >>= parseDecimal

            return!
                getElement "amount" data
                >>= getAttribute "unit"
                >>= getAttributeValue
                >>= function
                    | "mL" -> Choice1Of2 <| (Millilitres <| value * 1m<mL>)
                    | "g" -> Choice1Of2 <| (Grammes <| value * 1m<g>)
                    | x -> Choice2Of2 ("Unknown unit," + x + ", found in an amount XML element.")
        }

        let! chemical = getElement "chemical" data >>= ChemicalXmlConverter.ConvertFrom

        return { Chemical = chemical; Quantity = amount; }
    }
}