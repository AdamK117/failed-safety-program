module SafetyProgram.Core.IO.LocalSvc

open Services
open System.Xml.Linq
open System.IO
open SafetyProgram.Base.Helpers

// A standard implementation of a local file service for XElements.
let localSvc<'a> (generator : unit -> 'a) (converter : TwoWayConverter<_,_>) = {

    // Create a new document.
    New = fun () -> async { 
        let x = generator()
        return Some x, None
    }

    // Load a document from the local filesystem.
    Load = fun path -> async {
        return match File.Exists path with
                | true ->
                    let fileStream = File.OpenRead path
                    fileStream
                    |> XElement.Load
                    |> converter.ConvertFrom
                    |> Option.bind(fun data ->
                        Some(data, fileStream))
                | false -> None
    }
        

    // Save a document to the local filesystem.
    Save = fun (path, fileStream, data) -> async {
        let fs = 
            match fileStream with
            | Some fs -> fs
            | None -> 
                match File.Exists path with
                | true -> path |> File.OpenRead
                | false -> path |> File.Create
        
        return 
            data
            |> converter.ConvertTo
            >>= fun serializedData ->
                let xDoc = new XDocument(fs)
                xDoc.Add serializedData
                xDoc.Save fs
                Some(data, fs)
    }
}
    

