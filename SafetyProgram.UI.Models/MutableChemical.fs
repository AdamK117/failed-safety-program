namespace SafetyProgram.UI.Models

open SafetyProgram.Core.Models
open System.Collections.ObjectModel
open System.ComponentModel
open SafetyProgram.Base.Helpers

type GuiChemical(chemical) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable name = chemical.Name
    let nameChanged = new Event<_>()

    let hazards = 
        new ObservableCollection<_>(
            chemical.Hazards
            |> Seq.map (fun x-> 
                new GuiHazard(x)))

    let mutable selected = false
    let selectedChanged = Event<_>()

    member this.Name
        with get () = name
        and set x = 
            name <- x
            raisePropChanged propertyChangedEvent this "Name"
            nameChanged.Trigger name

    member this.NameChanged = nameChanged.Publish

    member this.Hazards
        with get () = hazards

    member this.Selected 
        with get () = selected
        and set x = 
            selected <- x

    member this.SelectedChanged = selectedChanged.Publish

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

module ChemicalHelpers = 

    let guiChemicalToChemical (guiChemical : GuiChemical) = {
        Name = guiChemical.Name
        Hazards = Seq.map HazardHelpers.guiHazardToModel guiChemical.Hazards
    }