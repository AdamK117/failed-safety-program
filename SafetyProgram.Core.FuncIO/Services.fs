module SafetyProgram.Core.FuncIO.Services

open System.IO

// A local service implementation
type LocalService<'a> = {
    New : unit -> (Option<'a> * Option<FileStream>)
    Load : string -> Option<('a * FileStream)>
    Save : string -> Option<FileStream> -> 'a -> Option<('a * FileStream)>
}