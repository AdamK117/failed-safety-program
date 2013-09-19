module SafetyProgram.Core.Models.Serialization.DocumentObjectXml

open System.Xml.Linq

let getDocumentObject (element : XElement) = 
    match element.Name.LocalName with
    | "chemicaltable" -> None
    | _ -> None