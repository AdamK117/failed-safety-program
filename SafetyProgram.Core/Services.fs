module SafetyProgram.Core.Services

open System.IO
open FSharpx
open System.Xml.Linq
open SafetyProgram.Base.Helpers
open FSharpx.Choice
open System

// Aliases for clearer API.
type ErrorMsg = string
type Path = string

// Data types (local, database, in-memory, etc.).
type FileInfo =
| LockedFile of Path * FileStream
| UnlockedFile of Path

type DataType = 
| Local of FileInfo
| BufferedData

// A local service implementation
type LocalService<'a> = {
    New : unit -> Async<Choice<'a, ErrorMsg>>
    Load : Path -> Async<Choice<'a * DataType, ErrorMsg>>
    Save : ('a * FileInfo) -> Async<Choice<DataType, ErrorMsg>>
}

// Service types.
type IoService<'a> =
| LocalSvc of LocalService<'a>

// A standard implementation of a local file service for XElements (XML).
let localSvc<'a> generator (converter : TwoWayConverter<'a,XElement>) = {

    New = fun () -> async { 
        let x = generator()
        return Choice1Of2 x
    }

    Load = fun pth -> async {
        return choose {
            let! fs = 
                match File.Exists pth with
                | true -> Choice1Of2(File.OpenRead pth)
                | false -> Choice2Of2("File '" + pth + "' not found.")

            let fileInfo = LockedFile(pth, fs)

            let! data = fs |> XElement.Load |> converter.ConvertFrom 

            return (data, Local(fileInfo))
        }
    }
        
    Save = fun (data, fileInfo) -> async {
        return choose {
            let pth, fs = 
                match fileInfo with
                | LockedFile(x,y) -> x,y
                | UnlockedFile x -> 
                    match File.Exists x with
                    | true -> x, File.OpenRead x
                    | false -> x, File.Create x

            let! convertedData = converter.ConvertTo data

            let xDocument = new XDocument(fs)
            xDocument.Add convertedData
            xDocument.Save fs

            return Local(LockedFile(pth, fs))
        }
    }
}