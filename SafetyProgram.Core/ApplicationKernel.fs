module SafetyProgram.Core

open System
open System.ComponentModel
open SafetyProgram.Core.Models
open SafetyProgram.Core.IO.Services
open SafetyProgram.Core.IO.LocalSvc
open SafetyProgram.Core.Models.Serialization.Core
open SafetyProgram.Core.Models.Serialization.DocumentXml
open System.Xml.Linq
open System.IO

// Data open in the application (could be local, could be databased).
type DataType = 
| LocalFile of FileStream
| BufferedFile

// Application state data.
type KernelData = {
    // The current document open in the application.
    Content : Option<Document * DataType>

    // The IO service used by the application.
    Service : DataService<Document>

    // The configuration used by the application.
    Configuration : ApplicationConfiguration
}

// Message passed back when attempting to modify the kernel.
type UpdateResult = Ok | Error of string

// Active service for monitoring and updating the application kernel.
type DataService<'a> = {
    Current : unit -> Async<'a>

    KernelDataChanged : IEvent<'a>

    Modify : ('a -> 'a) -> Async<UpdateResult>
}

/// Build a kernel service for the application kernel.
let buildKernelService<'a> (init : 'a) = 

    // Mutable: set the kernel data.
    let current = ref init

    // Track kernel data changes.
    let kernelDataChanged = new Event<'a>()

    // Serves incoming requests for the current kernel data.
    let currentData : MailboxProcessor<AsyncReplyChannel<'a>> = 
        MailboxProcessor.Start <| fun inbox ->
            let rec loop () = 
                async {
                    let! chn = inbox.Receive ()
                    chn.Reply current.Value
                    return! loop()
                }
            loop()

    // Serves incoming modify requests.
    let modifyProc : MailboxProcessor<('a -> 'a) * AsyncReplyChannel<UpdateResult>> = 
        MailboxProcessor.Start <| fun inbox ->
            let rec loop () = 
                async {
                    let! f, replyChannel = inbox.Receive()
                    let v = current.Value
                    try
                        current := f v
                        kernelDataChanged.Trigger current.Value
                        replyChannel.Reply UpdateResult.Ok
                    with
                    | e ->
                        replyChannel.Reply (UpdateResult.Error e.Message)

                    return! loop()
                }
            loop()
    {
        Current = fun () -> currentData.PostAndAsyncReply id
        KernelDataChanged = kernelDataChanged.Publish
        Modify = fun f -> modifyProc.PostAndAsyncReply(fun replyChannel -> f, replyChannel)
    }

// Default kernel state.
let defaultKernel = {
        Content = None
        Service = Local(localSvc<Document> (fun () -> { Content = Seq.empty; Format = { Width = 1m<m>; Height = 1m<m> } }) DocumentXml)
        Configuration = { ImplMe = false }
}