module SafetyProgram.Core.Models

type Hazard = {
    Warning : string
    SignalWord : string
    Symbol : string
    RiskPhrase : string  
}

type Chemical = {
    Name : string;
    Hazards : seq<Hazard>
}

// Define amounts used in terms of grammes (g) for solids
// or millilitres (mL) for liquids. Density(p) connects the two.
[<Measure>] type g
[<Measure>] type mL

type UsageQuantity = 
    | Grammes of decimal<g>
    | Millilitres of decimal<mL>

type CoshhChemical = {
    Chemical : Chemical
    Quantity : UsageQuantity
}

[<Measure>] type m

type Format = {
    Width : decimal<m>
    Height : decimal<m>
}

type ChemicalTable = {
    Header : string
    Chemicals : seq<CoshhChemical>
}

type DocumentObject = 
    | ChemicalTable of ChemicalTable

type Document = {
    Content : seq<DocumentObject>
    Format : Format
}

type ApplicationConfiguration = { ImplMe : bool }