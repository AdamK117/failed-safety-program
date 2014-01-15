namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.Views
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.Models
open System.Windows.Controls
open System.Collections.ObjectModel
open SafetyProgram.Core.Services
open SafetyProgram.Serialization.DocumentXml
open Fluent
open System

module Defaults = 

    let defaultKernelData =
        let documentGen () = { Content = Seq.empty; Format = { Width = 1m<m>; Height = 1m<m> }}
        let twoWay = DocumentXml
        let svc = localSvc<Document> documentGen twoWay
        new GuiKernelData(None, LocalSvc svc, {ImplMe = false})

type UiController(model : GuiKernelData) = 

    // Selection tabs
    let selectionTabs = function
    | GuiChemicalTableDocObj x -> 
        let vm = new ChemicalTableRibbonTabViewModel(x) :> IChemicalTableRibbonTabViewModel
        new ChemicalTableContextualRibbonTab(vm) :> RibbonTabItem

    // Ribbon tabs wireup
    let ribbonTabs = function
    | Some x -> 
        let insertTabVm = new InsertRibbonTabViewModel(x)
        let insertTabV = new InsertRibbonTabView(insertTabVm)
        let y = new ObservableCollection<_>()
        y.Add(insertTabV :> RibbonTabItem)
        y
    | None -> new ObservableCollection<_>() // No document open

    // Ribbon wireup
    let ribbonViewModel guiKernel = new RibbonViewModel(ribbonTabs, selectionTabs, guiKernel)
    let ribbonView viewModel = new DefaultRibbonView(viewModel)
    let ribbonVVM x = 
        let vm = ribbonViewModel x
        let v = ribbonView vm
        (v :> Ribbon, vm :> IViewModel)

    // DocumentObject view wireup
    let documentObjectVMM x = 
        match x with
        | GuiChemicalTableDocObj m -> 
            let vm = new ChemicalTableViewModel(m)
            let v = new DefaultChemicalTableView(vm)
            (v :> Control, vm :> IViewModel)

    // Content view wireup
    let contentViewModel doc = new DocumentViewModel(documentObjectVMM, doc)
    let contentView vm = new DocumentView(vm)
    let contentVVM m = 
        match m with
        | Some x -> 
            let vm = contentViewModel x
            let v = contentView vm
            (v :> Control, vm :> IViewModel)
        | None -> 
            let vm = new NoDocumentViewModel()
            let v = new NoDocumentView()
            (v :> Control, vm :> IViewModel)

    let mainViewModel = new MainViewModel(ribbonVVM, contentVVM, model)
    let mainView = new DefaultMainView(mainViewModel)

    member x.View = (mainView :> Window)

    interface IDisposable with
        member this.Dispose() =
            ()