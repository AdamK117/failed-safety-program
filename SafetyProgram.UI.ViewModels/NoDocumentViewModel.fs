namespace SafetyProgram.UI.ViewModels

type NoDocumentViewModel() =

    let propertyChanged = new Event<_,_>()

    interface INoDocumentViewModel with

        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

        member this.Dispose() = ()

    // IMPLICIT IMPL
    [<CLIEvent>]
    member this.PropertyChanged = propertyChanged.Publish

    member this.Dispose() = ()