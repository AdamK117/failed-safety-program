namespace SafetyProgram.UI.ViewModels.ViewModelInterface

open SafetyProgram.Core

// Defines an interface for a viewmodel.
type IViewModel<'a> = 

    // Push a new model for the viewmodel to represent.
    abstract member PushModel : 'a -> unit 

    // Occurs when a viewmodel requests a manipulation on its model.
    abstract member CommandRequested : IEvent<'a->'a>