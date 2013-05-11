namespace SafetyProgram.RepositoryIO

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ConfigFileGetter

[<TestClass>]
type UnitTest() = 
    [<TestMethod>]
    member this.TestMethod1 () = 
        Assert.AreEqual(1,1)
