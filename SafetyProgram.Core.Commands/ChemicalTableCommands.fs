module ChemicalTableCommands

open System
open SafetyProgram.Core.Models
open SafetyProgram.Core.CommandCore

let addChemical = {
    CanExecute = fun _ ->
        true
    Execute = fun chemTable ->
        { chemTable
            with Chemicals = chemicalTable.Chemicals
                |> Seq.append([{ Chemical=chemical; Quantity=Grammes(0m<g>)}]) 
        }
}

let removeChemical index = {
    CanExecute = fun chemTable ->
        Seq
}