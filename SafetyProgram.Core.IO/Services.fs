module SafetyProgram.Core.IO.Services

open System.IO
open System.Xml.Linq

// A local service implementation
type LocalService<'a> = {
    New : (unit->'a) -> (Option<'a> * Option<FileStream>)
    Load : XElement->Option<'a> -> string -> Option<('a * FileStream)>
    Save : 'a->Option<XElement> -> string -> Option<FileStream> -> 'a -> Option<('a * FileStream)>
}

type DataService<'a> =
    | Local of LocalService<'a>