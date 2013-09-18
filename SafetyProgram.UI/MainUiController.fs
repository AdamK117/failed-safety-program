namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Base
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels

type MainUiController(kernelData : KernelData, kernelChanged : IEvent<KernelData>) = 

    let view = new DefaultMainView(
        new MainViewModel(
            kernelData,
            null,
            new DefaultRibbonView(
                new RibbonViewModel(
                    kernelData.Document,
                    null,
                    getDocumentRibbonTabs)),
            documentViews))

    member this.View = view :> Window

