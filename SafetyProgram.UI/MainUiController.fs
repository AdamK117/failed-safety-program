namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Core
open SafetyProgram.Core.Models
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels
open SafetyProgram.UI.ViewModels.Core

type MainUiController(svc) = 
    let currentData = Async.RunSynchronously <| svc.Current()

    let ribbonViewModel = new RibbonViewModel(svc, docRibbonTabUiFactory)
    let ribbonView = new DefaultRibbonView(ribbonViewModel)

    let curryFac = docUiFactory(generateSubService svc (fun a-> a.Content) (fun f -> fun content -> { content with Content = f content.Content }))
    let mainViewModel = new MainViewModel(svc, ribbonView, curryFac)
    let mainView = new DefaultMainView(mainViewModel)

    member x.View = mainView :> Window