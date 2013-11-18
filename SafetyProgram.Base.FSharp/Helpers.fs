module SafetyProgram.Base.FSharp.Helpers

open System
open System.ComponentModel

let NullCheck([<ParamArray>] arr : 'a array) = 
    arr
    |> Array.iter(fun a ->
        if a = null then
            raise(new ArgumentNullException()))

let raisePropChanged (event : Event<PropertyChangedEventHandler, PropertyChangedEventArgs>) sender args =
    event.Trigger(sender, new PropertyChangedEventArgs("ContentView"))