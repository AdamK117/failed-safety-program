namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open System

type IViewModel = 
    inherit INotifyPropertyChanged
    inherit IDisposable