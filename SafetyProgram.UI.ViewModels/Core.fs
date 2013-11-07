module SafetyProgram.UI.ViewModels.Core

open System.ComponentModel

let raisePropChanged (event : Event<PropertyChangedEventHandler, PropertyChangedEventArgs>) sender args =
    event.Trigger(sender, new PropertyChangedEventArgs("ContentView"))

