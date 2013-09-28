module SafetyProgram.Core

open SafetyProgram.Core.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Core.IO.LocalSvc
open SafetyProgram.Core.Models.Serialization.Core
open SafetyProgram.Core.Models.Serialization.DocumentXml
open System.Xml.Linq
open System.IO

// Application state data.
type KernelData = {
    Document : Option<Document>
    Configuration : ApplicationConfiguration
}

// Actual application kernel.
type ApplicationKernel() = 
    // Creates default document.
    let documentGenerator = fun () -> 
        { Content = Seq.empty;
            Format = { Width=0.21m<m>; Height=0.297m<m> } }

    // Holder for the current data.
    let mutable kernelData = { 
        Document = Some <| documentGenerator();
        Configuration = { ImplMe = true }
    }

    // First class event for publishing data changes.
    let kernelDataChanged = new Event<KernelData>()

    // Default service implementation.
    let ioService = localSvc<Document> documentGenerator DocumentXml    

    member this.KernelData = kernelData

    member this.KernelDataChanged = kernelDataChanged.Publish

    member this.PerformCommand(command : KernelData->KernelData) =
        kernelData <- command(kernelData)
        kernelDataChanged.Trigger(kernelData)

    member this.Service = ioService