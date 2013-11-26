module SafetyProgram.Core.IO.LocalSvc

open Services
open System.Xml.Linq
open System.IO
open SafetyProgram.Base.Helpers
open FSharpx.Choice
open System

// A standard implementation of a local file service for XElements (XML).
let localSvc<'a> generator (converter : TwoWayConverter<'a,XElement>) = {

    New = fun () -> async {  return Choice2Of3 (generator()) }

    Load = fun path -> async {
        let x = 
            match File.Exists path with
            | true ->
                let fileStream = File.OpenRead path
                fileStream
                |> XElement.Load
                |> converter.ConvertFrom 
                >>= fun y -> Choice1Of2 (y, fileStream)
            | false -> Choice2Of2 ("File '" + path + "' not found.")

        return x
    }
        
    Save = fun (path, fileStream, data) -> async {
        let fs = 
            match fileStream with
            | Some x -> x
            | None -> 
                match File.Exists path with
                | true -> File.OpenRead path
                | false -> File.Create path
        
        return 
            converter.ConvertTo data
            >>= fun serializedData ->
                let xDoc = new XDocument(fs)
                xDoc.Add serializedData
                xDoc.Save fs
                Choice1Of2(data, fs)
    }
}