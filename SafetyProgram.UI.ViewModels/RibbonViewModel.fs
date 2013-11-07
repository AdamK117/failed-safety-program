namespace SafetyProgram.UI.ViewModels

open SafetyProgram.Core.Models
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.Core
open System.Windows.Input
open NewDocumentCommand
open SafetyProgram.UI.ViewModels.Core

type RibbonViewModel(svc, tabFactory) as this = 

    let ribbonTabGenerator (mo : Option<Document * DataType>) =
        match mo with
        | Some(m, _) -> tabFactory m
        | None -> Seq.empty

    let tabViewGenerator = ribbonTabGenerator >> Seq.map fst

    let propertyChangedEvent = new Event<_,_>() 

    let mutable currentModel =
        svc.Current()
        |> Async.RunSynchronously

    let mutable ribbonTabs = tabViewGenerator currentModel.Content

    do
        svc.KernelDataChanged.Add(fun newModel ->
            let oldModel = currentModel
            currentModel <- newModel

            if currentModel.Content <> oldModel.Content then
                raisePropChanged propertyChangedEvent this "RibbonTabs")

    interface IRibbonViewModel with
        member this.RibbonTabs = 
            ribbonTabGenerator currentModel.Content
            |> Seq.map fst

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish

        member this.OpenDocument = new NewDocument(svc) :> ICommand

    // Expliit interface impl
    member this.RibbonTabs = ribbonTabGenerator currentModel.Content
    [<CLIEvent>]
    member this.PropertyChanged = propertyChangedEvent.Publish

    member this.OpenDocument = new NewDocument(svc) :> ICommand