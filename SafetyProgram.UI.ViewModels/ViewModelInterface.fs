namespace SafetyProgram.UI.ViewModels.ViewModelInterface

open SafetyProgram.Core

// Defines an interface for a viewmodel.
type IViewModel<'a> = 
    abstract member PushModel : 'a -> unit 
    abstract member CommandRequested : IEvent<'a->'a>