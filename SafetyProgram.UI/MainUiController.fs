namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels

type MainUiController(kernel : ApplicationKernel) = 
    member this.a = true