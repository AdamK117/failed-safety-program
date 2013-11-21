namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiDocumentObject = 
    | GuiChemicalTable of GuiChemicalTable

module DocumentObjectHelpers = 

    let guiDocumentObjectFactory docObject = 
        match docObject with
        | ChemicalTable x -> GuiChemicalTable(new GuiChemicalTable(x)) 

    let guiDocumentObjectToDocumentObject guiDocObject = 
        match guiDocObject with
        | GuiChemicalTable x ->
            ChemicalTable(
                {   
                    Header = x.Header
                    Chemicals = Seq.map CoshhChemicalHelpers.guiCoshhChemicalToCoshhChemical x.Chemicals            
                })