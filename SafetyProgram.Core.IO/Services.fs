module SafetyProgram.Core.IO.Services

open System.IO
open System.Xml.Linq

// A local service implementation
type LocalService<'a> = {
    New : unit -> (Option<'a> * Option<FileStream>)
    Load : string -> Option<('a * FileStream)>
    Save : string -> Option<FileStream> -> 'a -> Option<('a * FileStream)>
}

// A holder for a dataservice.
type DataService<'a> =
    | Local of LocalService<'a>