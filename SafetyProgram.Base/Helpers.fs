module SafetyProgram.Base.Helpers

open System
open System.ComponentModel
open System.Xml.Linq
open System
open FSharpx.Choice

let NullCheck([<ParamArray>] arr : 'a array) = 
    arr
    |> Array.iter(fun a ->
        if a = null then
            raise(new ArgumentNullException()))

let raisePropChanged (event : Event<PropertyChangedEventHandler, PropertyChangedEventArgs>) sender args =
    event.Trigger(sender, new PropertyChangedEventArgs("ContentView"))

// Defines a two-way converter for converting
// data to or from in-memory/external storage.
type TwoWayConverter<'a, 'b> = {
    ConvertTo : 'a -> Choice<'b, string>
    ConvertFrom : 'b -> Choice<'a, string>
}

// Converts a string to an XName for LINQ XML.
let xname s = XName.Get(s)

// Attempts to get an element from an XElement
let getElement elementName (element : XElement) = 
    match element.Element(xname elementName) with
    | null -> Choice2Of2 ("XML element with name " + elementName + "not found.")
    | x -> Choice1Of2 x

// Tries to get an attribute from an XElement.
let getAttribute attributeName (element : XElement) =
    match element.Attribute(xname attributeName) with
    | null -> Choice2Of2 ("XML attribute with name " + attributeName + "not found.")
    | x -> Choice1Of2 x

// Tries to get the value of an XElement.
let getValue (element : XElement) = Choice2Of2 element.Value

let getAttributeValue (element : XAttribute) = Choice2Of2 element.Value

// Tries to parse a string to a decimal.
let parseDecimal x = 
    try
        Choice1Of2 (Decimal.Parse x)
    with
        | :? System.ArgumentNullException -> Choice2Of2 "Tried to parse null decimal"
        | :? System.IO.InvalidDataException -> Choice2Of2 "Invalid data parsed (decimal)"