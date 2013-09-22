module SafetyProgram.Core

open SafetyProgram.Core.Models
open SafetyProgram.Core.IO.Services

// Application state data.
type KernelData = {
    Document : Option<Document>
    Configuration : ApplicationConfiguration
}

type ApplicationKernel = {
    mutable KernelData : KernelData
    KernelDataChanged : IEvent<KernelData>
    IOService : DataService<Document>
    PerformCommand : KernelData->KernelData->unit
}

type applicationKernel(config) = 
    let mutable kernelData = { Document = None; Configuration = config }
    let ketnelDataChanged = new Event<KernelData>()
    let service = 

    member this.KernelData = 
 

    

