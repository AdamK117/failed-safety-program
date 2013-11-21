module SafetyProgram.Base.Helpers

open System
open System.ComponentModel
open System.Xml.Linq
open System

let NullCheck([<ParamArray>] arr : 'a array) = 
    arr
    |> Array.iter(fun a ->
        if a = null then
            raise(new ArgumentNullException()))

let raisePropChanged (event : Event<PropertyChangedEventHandler, PropertyChangedEventArgs>) sender args =
    event.Trigger(sender, new PropertyChangedEventArgs("ContentView"))

// Defines a two-way converter for converting data to
// or from in-memory/external storage.
type TwoWayConverter<'a, 'b> = {
    ConvertTo : 'a->Option<'b>
    ConvertFrom : 'b->Option<'a>
}

// Basic maybe monad implementation
type MaybeBuilder() = 
    member this.Bind(m, f) = Option.bind f m
    member this.Return(x) =
        Some x
    member this.ReturnFrom(x) = 
        x

// Instance of maybe monad implementation.
let maybeBuilder = new MaybeBuilder()

// Converts a string to an XName for LINQ XML.
let xname s = XName.Get(s)

// Attempts to get an element from an XElement
let getElement (element : XElement) elementName = 
    match element.Element(xname elementName) with
    | null -> None
    | elem -> Some elem

// Tries to get an attribute from an XElement.
let getAttribute (element : XElement) attributeName =
    match element.Attribute(xname attributeName) with
    | null -> None
    | attr -> Some attr

// Tries to get the value of an XElement.
let getValue (element : XElement) =
    Some element.Value

// Tries to get the value of an XAttribute.
let getAttributeValue (attribute : XAttribute) = 
    Some attribute.Value

// Tries to parse a string to a decimal.
let parseDecimal x = 
    try
        Some <| Decimal.Parse(x)
    with
        | :? System.ArgumentNullException -> None
        | :? System.IO.InvalidDataException -> None

// Shorthand bind notation for options.
let (>>=) m f = 
    Option.bind f m

// Flatten a sequence of options into an option sequence. Will
// return 'None' if the sequence contains a single 'None'
let flattenOptions (s : seq<option<'a>>) : option<seq<'a>> =
    s
    |> Seq.exists(fun item -> item = None)
    |> function
        | true -> None
        | false -> 
            s 
            |> Seq.map(fun item -> item.Value) 
            |> Some