module SafetyProgram.Serialization.DocumentXml

open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open DocumentObjectXml
open SafetyProgram.Base.Helpers
open FSharpx.Choice

let DocumentXml : TwoWayConverter<Document, XElement> = {
    ConvertTo = fun _ -> new NotImplementedException() |> raise
    ConvertFrom = fun data -> choose {
        let format = { Width=0.21m<m>; Height=0.297m<m> }

        let! content = 
            data.Elements()
            |> Seq.toList
            |> mapM DocumentObjectXmlConverter.ConvertFrom

        return { Content=content; Format=format; }
    }        
}