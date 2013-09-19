module SafetyProgram.Core.Models.Serialization.DocumentXml

open Helpers
open ConverterInterface
open SafetyProgram.Core.Models
open System.Xml.Linq
open System.Xml
open System

type DocumentXml() = 
    interface IConverter<Document, XElement> with
        member this.ConvertTo(data : Document) = 
            raise (new NotImplementedException())
        member this.ConvertFrom(data : XElement) = 
            let format = { Width=0.21m<m>; Height=0.297m<m> }
            let content = 
                data.Elements()
                |> Seq.map (fun _ -> None)
                |> Some

            if (content <> None) then
                None
            else
                None
