namespace SafetyProgram.UI.ViewModels

open Fluent
open System.Windows.Controls
open System.ComponentModel

type IMainViewModel = 

    inherit IViewModel

    abstract RibbonView : Ribbon with get
    abstract ContentView : Control with get
    abstract Commands : MainCommands with get