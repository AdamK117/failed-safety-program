module NewDocumentCommand

open System.Windows.Input
open SafetyProgram.Core
open SafetyProgram.Core.Models

type NewDocument(svc) = 

    let canExecuteChangedEvent = new Event<_,_>()

    let mutable currentModel = svc.Current() |> Async.RunSynchronously

    do
        svc.KernelDataChanged.Add(fun newModel ->
            currentModel<-newModel)

    interface ICommand with
        member this.CanExecute(param) =
            true

        member this.Execute(param) =
            let newDocFunc (oldKernel : KernelData) = 
                { oldKernel with
                    Content = Some({Content = Seq.empty; Format = { Width = 1m<m>; Height = 1m<m>}}, BufferedFile)}
            svc.Modify newDocFunc |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChangedEvent.Publish

