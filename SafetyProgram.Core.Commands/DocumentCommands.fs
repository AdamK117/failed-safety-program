module DocumentCommands

open System
open SafetyProgram.Core.Models

let deleteSelected a = 
    new NotImplementedException() |> raise

let insertChemicalTable document =
    { document with 
        Content = document.Content
        |> Seq.append([ChemicalTable({ Header = "Default Header"; Chemicals = Seq.empty })])
    }

let deleteDocumentObject document documentObject = 
    new NotImplementedException() |> raise