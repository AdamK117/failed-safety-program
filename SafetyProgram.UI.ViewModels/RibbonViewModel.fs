namespace SafetyProgram.UI.ViewModels

open System.ComponentModel
open Fluent
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels.ViewModelInterface
open SafetyProgram.Core
open System.Windows.Input

type NewDocument(tun) = 

    let canExecuteChangedEvent = new Event<_,_>()

    interface ICommand with
        member this.CanExecute(param) =
            false

        member this.Execute(param) =
            let newDocFunc oldDoc = 
                None
            tun newDocFunc

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChangedEvent.Publish

type RibbonViewModel(model, tabFactory : Document -> seq<RibbonTabItem * IViewModel<Document>>) as this = 

    let propertyChangedEvent = new Event<_,_>()
    let commandRequest = new Event<_>()
    let mutable currentModel = model

    let ribbonTabGenerator (mo : Option<Document * DataType>) =
        match mo with
        | Some(m, _) -> tabFactory m
        | None -> Seq.empty 

    interface IViewModel<KernelData> with
        // New model visit
        member this.PushModel(newModel) = 
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel <> oldModel then
                propertyChangedEvent.Trigger(
                    this,
                    new PropertyChangedEventArgs("RibbonTabs"))            

        member this.CommandRequested = 
            commandRequest.Publish

    interface IRibbonViewModel with
        member this.RibbonTabs = 
            ribbonTabGenerator currentModel
            |> Seq.map fst
        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

        member this.OpenDocument = 
            let tun f = 
                commandRequest.Trigger(f)
            new NewDocument(tun) :> ICommand

    member this.RibbonTabs = ribbonTabGenerator currentModel
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = 
        let tun f = 
            commandRequest.Trigger(f)
        new NewDocument(tun) :> ICommand