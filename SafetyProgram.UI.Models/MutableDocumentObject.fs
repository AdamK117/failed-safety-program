namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiDocumentObject = 
    | GuiChemicalTableDocObj of GuiChemicalTable

module DocumentObjectHelpers = 

    let guiDocumentObjectFactory docObject = 
        match docObject with
        | ChemicalTable x -> GuiChemicalTableDocObj(new GuiChemicalTable(x))

    let guiDocumentObjectToDocumentObject guiDocObject = 
        match guiDocObject with
        | GuiChemicalTableDocObj x ->
            ChemicalTable(
                {   
                    Header = x.Header
                    Chemicals = Seq.map CoshhChemicalHelpers.guiCoshhChemicalToCoshhChemical x.Chemicals            
                })