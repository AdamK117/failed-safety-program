module ChemicalTableCommands

open System
open SafetyProgram.Core.Models

// Add a chemical to the chemicaltable.
let addChemical chemicalTable chemical = 
    { chemicalTable 
        with Chemicals = chemicalTable.Chemicals
            |> Seq.append([{ Chemical=chemical; Quantity=Grammes(0m<g>)}]) 
    }

let removeChemical chemicalTable chemical =
    new NotImplementedException() |> raise