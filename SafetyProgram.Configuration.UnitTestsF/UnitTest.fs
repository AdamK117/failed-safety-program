namespace UnitTestProject1

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Xml
open System.Xml.Linq
open SafetyProgram.Static
open SafetyProgram.Configuration
open SafetyProgram.Base
open SafetyProgram.ModelObjects

[<TestClass>]
type LocalConfigurationFileTestt() =
    let getTestData = 
        XElement.Load(TestData.CONFIGURATION_FILE)

    let getInvalidTestData = 
        XElement.Load(TestData.INVALID_CONFIGURATION_FILE)

    let getFactory = 
        new ConfigurationLocalFileFactory(
            new RepositoryInfoLocalFileFactory(),
            new ChemicalModelObjectLocalFileFactory()
        )
 
    [<TestMethod>]
    member x.LoadFileTest() =
        let data = getTestData
        let factory = getFactory
        let loadedConfig = factory.Load(data)

        Assert.AreEqual<bool>(loadedConfig.DocumentLock, false)
