namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels

type MainUiController(kernelService : DataService<KernelData>) = 
    let currentData = Async.RunSynchronously <| kernelService.Current()

    let ribbonVm = new RibbonViewModel(currentData.Content, docRibbonTabUiFactory)

    let theView = new DefaultMainView(
                    new MainViewModel(
                        currentData,
                        new DefaultRibbonView(
                            new RibbonViewModel(
                                currentData.Content, docRibbonTabUiFactory)),
                        docUiFactory))

    member x.View = theView :> Window