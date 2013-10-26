module SafetyProgram.UI.ViewModels.Core

open System.ComponentModel

let raisePropChanged (event : Event<PropertyChangedEventHandler, PropertyChangedEventArgs>) sender args =
    event.Trigger(sender, new PropertyChangedEventArgs("ContentView"))

// Defines an interface for a viewmodel.
type IViewModel<'a> = 

    // Push a new model for the viewmodel to represent.
    abstract member PushModel : 'a -> unit 

    // Occurs when a viewmodel requests a manipulation on its model.
    abstract member CommandRequested : IEvent<'a->'a>

