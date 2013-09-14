namespace SafetyProgram.UI

open System.Windows
open SafetyProgram.Base
open SafetyProgram.Core
open SafetyProgram.Core.Commands.SelectionLogic
open SafetyProgram.UI.ContentViewGenerators
open SafetyProgram.UI.RibbonViewGenerators
open SafetyProgram.UI.Views.MainViews
open SafetyProgram.UI.ViewModels

type MainUiController(applicationKernel : IApplicationKernel) =
    let commandInvoker = new CommandController()
    let selectionManager = new SelectionManager()
    
    let documentViewGenerator = getDocumentViews applicationKernel.Configuration commandInvoker selectionManager
    let ribbonViewGenerator = getDocumentRibbonTabs commandInvoker
     
    let view = new DefaultMainView(
                new MainViewModel(
                    applicationKernel,
                    new DefaultRibbonView(
                        new RibbonViewModel(
                            applicationKernel,
                            applicationKernel.Configuration,
                            commandInvoker :> ICommandController,
                            selectionManager :> ISelectionManager,
                            getDocumentRibbonTabs commandInvoker) :> IRibbonViewModel),
                    documentViewGenerator) :> IMainViewModel)

    member this.View = view :> Window

