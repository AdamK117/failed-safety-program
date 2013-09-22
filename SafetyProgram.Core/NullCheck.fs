module NullCheck

open System

let NullCheck ([<ParamArray>] arr : 'a array) = 
    arr
    |> Seq.iter(fun item ->
        if item = null then
            new ArgumentNullException() |> raise)

