module NewDocumentCommand

open System.Windows.Input
open SafetyProgram.Core
open SafetyProgram.Core.Models

type NewDocument(tun) = 

    let canExecuteChangedEvent = new Event<_,_>()

    interface ICommand with
        member this.CanExecute(param) =
            true

        member this.Execute(param) =
            let newDocFunc (oldKernel : KernelData) = 
                { oldKernel with
                    Content = Some({Content = Seq.empty; Format = { Width = 1m<m>; Height = 1m<m>}}, BufferedFile)
                }
            tun newDocFunc

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChangedEvent.Publish

