namespace SafetyProgram.UI.ViewModels

open System.Collections.ObjectModel
open System.ComponentModel
open System.Windows.Controls
open ReactiveUI

type IDocumentViewModel =

    inherit IViewModel
    
    abstract Width : decimal with get
    abstract Height : decimal with get
    abstract DocumentObjects : IReactiveDerivedList<Control> with get