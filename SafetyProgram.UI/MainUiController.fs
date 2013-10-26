namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.ViewModels.Core

type MainUiController(kernelService : DataService<KernelData>) = 
    let currentData = Async.RunSynchronously <| kernelService.Current()

    let ribbonViewModel = new RibbonViewModel(currentData, docRibbonTabUiFactory)
    let ribbonView = new DefaultRibbonView(ribbonViewModel)

    let mainViewModel = new MainViewModel(currentData, ribbonView, docUiFactory)
    let mainView = new DefaultMainView(mainViewModel)

    do
        (ribbonViewModel :> IViewModel<KernelData>).CommandRequested.Add(fun command -> 
            kernelService.Modify(command)
            |> Async.RunSynchronously
            |> ignore)

        (mainViewModel :> IViewModel<KernelData>).CommandRequested.Add(fun command ->
            kernelService.Modify(command)
            |> Async.RunSynchronously
            |> ignore)

        kernelService.KernelDataChanged.Add(fun newData->
            (mainViewModel :> IViewModel<KernelData>).PushModel(newData)
            (ribbonViewModel :> IViewModel<KernelData>).PushModel(newData))

    member x.View = mainView :> Window