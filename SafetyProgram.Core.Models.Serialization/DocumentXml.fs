module SafetyProgram.Core.Models.Serialization.DocumentXml

open Helpers
open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System
open DocumentObjectXml

let DocumentXml = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) -> 
        let format = { Width=0.21m<m>; Height=0.297m<m> }

        let content = 
            data.Elements()
            |> Seq.map (DocumentObjectXmlConverter.ConvertFrom)
            |> Some

        if (content <> None) then
            None
        else
            None
}
