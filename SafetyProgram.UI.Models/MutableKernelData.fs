namespace SafetyProgram.UI.Models

open System.ComponentModel
open SafetyProgram.Base.Helpers
open SafetyProgram.Core.IO.Services
open SafetyProgram.Core.Models

type GuiKernelData(vcontent : Option<ContentHolder>, vservice : IoService<Document>, vconfiguration : ApplicationConfiguration) = 

    let propertyChangedEvent = new Event<_,_>()

    let mutable content = vcontent
    let contentChanged = new Event<_>()

    let mutable service = vservice
    let serviceChanged = new Event<_>()

    let mutable configuration = vconfiguration
    let configurationChanged = new Event<_>()

    member this.Content 
        with get () = content
        and set x = 
            content <- x
            raisePropChanged propertyChangedEvent this "Content"
            contentChanged.Trigger content

    member this.ContentChanged = contentChanged.Publish

    member this.Service
        with get () = service
        and set x = 
            service <- x
            raisePropChanged propertyChangedEvent this "Service"
            serviceChanged.Trigger service

    member this.ServiceChanged = serviceChanged.Publish

    member this.Configuration
        with get () = configuration
        and set x = 
            configuration <- x
            raisePropChanged propertyChangedEvent this "Configuration"
            configurationChanged.Trigger configuration

    member this.ConfigurationChanged = configurationChanged.Publish

    interface INotifyPropertyChanged with

        [<CLIEvent>]
        member this.PropertyChanged = propertyChangedEvent.Publish