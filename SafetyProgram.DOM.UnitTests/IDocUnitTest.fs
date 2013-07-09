namespace SafetyProgram.DOM.UnitTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open SafetyProgram.DOM
open SafetyProgram.Base.Interfaces
open Moq

//Defines a class for testing IDoc implementations
type IDocUnitTests =
    static member private CheckChangeFormat (iDocConstructor : unit->IDoc) = 
        let iDoc = iDocConstructor()

        //Check if the class was initialized into an invalid state
        Assert.IsNotNull(iDoc.Format, "The document format must never be null!")

        let newFormat = new Mock<IFormat>()

        //Define a hook that will test the changed format.
        let hookTriggered = false
        let eventChecker eventFormat =
            let hookTriggered = true
            Assert.AreEqual(newFormat, eventFormat)

        //Add the defined hook to the event.
        iDoc.FormatChanged.Add(eventChecker)

        //Change the format.
        iDoc.ChangeFormat(newFormat.Object)

        Assert.IsTrue(hookTriggered)

    static member CheckFormat (iDoc : IDoc) = 
        Assert.IsNotNull(iDoc.Format, "The document format must never be null!")

    static member TestIDoc (iDocConstructor : unit->IDoc) = 
        let iDoc = iDocConstructor()
        ()
        //CheckFormat(iDoc)
