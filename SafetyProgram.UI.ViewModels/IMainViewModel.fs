namespace SafetyProgram.UI.ViewModels

open Fluent
open System.Windows.Controls

type IMainViewModel = 
    abstract RibbonView : Ribbon with get, set
    abstract ContentView : Control with get