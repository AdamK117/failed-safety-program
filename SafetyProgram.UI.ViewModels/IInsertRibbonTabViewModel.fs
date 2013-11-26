namespace SafetyProgram.UI.ViewModels

type IInsertRibbonTabViewModel =
    
    inherit IViewModel

    abstract Filler : string
    abstract Commands : InsertTabCommands