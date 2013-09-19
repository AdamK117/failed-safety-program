﻿module SafetyProgram.Core.Models.Serialization.CoshhChemicalXml

open Helpers
open ChemicalXml
open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

let CoshhChemicalXmlConverter = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) -> 
        // Required: Get the quantity of chemical used.
        let amount = 
            // Get the amount element.
            match data.Element(xname "amount") with
            | null -> None
            | amountElement -> 
                // Get the unit attribute.
                match amountElement.Attribute(xname "unit") with
                | null -> None
                | unit ->
                    match unit.Value with
                    | "mL" -> 
                        try
                            Some <| (Millilitres <| Decimal.Parse(amountElement.Value) * 1m<mL>)
                        with 
                            | :? System.ArgumentNullException -> None
                            | :? System.IO.InvalidDataException -> None
                    | "g" ->
                        try
                            Some <| (Grammes <| Decimal.Parse(amountElement.Value) * 1m<g>)
                        with 
                            | :? System.ArgumentNullException -> None
                            | :? System.IO.InvalidDataException -> None
                    | _ -> None

        let chemical = 
            match data.Element(xname "chemical") with
            | null -> None
            | chemicalElement -> 
                let chemicalConverter = ChemicalXmlConverter
                chemicalElement |> chemicalConverter.ConvertFrom

        if (amount <> None) && (chemical <> None) then
            Some <| { Chemical=chemical.Value; Quantity=amount.Value }
        else
            None
}