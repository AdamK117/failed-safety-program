namespace SafetyProgram.Models.UnitTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open SafetyProgram.Models
open SafetyProgram.Base.Interfaces
open Moq
open System.Collections.ObjectModel

[<TestClass>]
type DocUnitTest() = 

    [<TestMethod>]
    member this.TestMethod1 () = 
        // Make a paramaterless ctor for the Doc being tested for general tests
        let ctor = (fun () -> 
            // Tuple of mock constructor params
            (new Mock<ObservableCollection<IDocObj>>(), new Mock<IFormat>()) 
            // Inject into the Doc implementation
            |> fun(arg1, arg2) -> new Doc(arg1.Object, arg2.Object) :> IDoc)
        
        // Perform general tests
        ctor |> IDocUnitTests.TestIDoc

