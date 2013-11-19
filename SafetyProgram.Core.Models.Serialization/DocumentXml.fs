module SafetyProgram.Core.Models.Serialization.DocumentXml

open SafetyProgram.Core.Models
open System.Xml.Linq
open System
open DocumentObjectXml
open SafetyProgram.Base.FSharp.Helpers

let DocumentXml = {
    ConvertTo = fun data ->
        new NotImplementedException() |> raise

    ConvertFrom = fun (data : XElement) -> 
        maybeBuilder {
            let format = { Width=0.21m<m>; Height=0.297m<m> }

            let! content = 
                data.Elements()
                |> Seq.map DocumentObjectXmlConverter.ConvertFrom
                |> flattenOptions

            return { Content=content; Format=format; }
        }
        
}
