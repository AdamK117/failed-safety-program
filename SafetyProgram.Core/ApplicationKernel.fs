module SafetyProgram.Core

open System.IO
open SafetyProgram.Core.Models

// A local service implementation
type LocalService<'a> = {
    New : unit -> (Option<'a> * Option<FileStream>)
    Load : string -> Option<('a * FileStream)>
    Save : string -> Option<FileStream> -> 'a -> Option<('a * FileStream)>
}

// Application state data.
type KernelData = {
    Document : Option<Document>
    Configuration : ApplicationConfiguration
}
 

    

