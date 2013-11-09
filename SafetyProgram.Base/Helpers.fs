module SafetyProgram.Base.Helpers

open System

let NullCheck([<ParamArray>] arr : 'a array) = 
    arr
    |> Array.iter(fun a ->
        if a = null then
            raise(new ArgumentNullException()))
