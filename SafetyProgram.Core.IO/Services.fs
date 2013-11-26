module SafetyProgram.Core.IO.Services

open System.IO
open FSharpx

// A local service implementation
type LocalService<'a> = {
    New : unit -> Async<Choice<'a * FileStream, 'a, string>>
    Load : string -> Async<Choice<'a * FileStream, string>>
    Save : string * Option<FileStream> * 'a -> Async<Choice<'a * FileStream, string>>
}

// A holder for a dataservice.
type IoService<'a> =
| LocalSvc of LocalService<'a>

// Data open in the application (could be local, could be databased).
type DataType = 
| LocalFile of Choice<string * FileStream, string>
| BufferedFile