module SafetyProgram.Core.IO.Services

open System.IO
open System.Xml.Linq

// A local service implementation
type LocalService<'a> = {
    New : unit -> Async<Option<'a> * Option<FileStream>>
    Load : string -> Async<Option<'a * FileStream>>
    Save : string * Option<FileStream> * 'a -> Async<Option<'a * FileStream>>
}

// A holder for a dataservice.
type IoService<'a> =
| LocalSvc of LocalService<'a>

// Data open in the application (could be local, could be databased).
type DataType = 
| LocalFile of FileStream
| BufferedFile