using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.Base.UnitTests
{
    [TestClass]
    public class LocalFileServiceMultiItemTest
    {
        public sealed class MockObject
        {
            public MockObject(string name)
            {
                this.Name = name;
            }

            public string Name
            {
                get;
                private set;
            }
        }

        private IList<MockObject> mockEntries = new List<MockObject>()
            {
                new MockObject("Superman"),
                new MockObject("Ironman"),
                new MockObject("Wonder Woman"),
                new MockObject("Captain America"),
                new MockObject("Green Goblin")
            };

        [TestMethod]
        public void LoadTest()
        {
            // Expected Behaviours
            //  -1) The service will load XML file provided in the path
            //      Test: Test the XElements being passed into the factory
            //  -2) It will navigate to the supplied parent node and use the factory to load in individual items.
            //      Test: Ensure the correct XElements are being passed into the factory

            //Used to monitor which results were passed
            var passedResults = new List<MockObject>(mockEntries);            

            //SETUP: Mock factory
            var mockFactory = new Mock<ILocalFileFactory<MockObject>>();

            mockFactory
                //Setup the load method
                .Setup<MockObject>(
                    ((mockFac) =>
                        mockFac.Load(
                            It.IsAny<XElement>()
                        )
                    )
                )
                //Return a bogus item
                .Returns<XElement>(
                    ((passedXElement) =>
                        {
                            //Check the correct node was passed
                            Assert.AreEqual<string>("mockitem", passedXElement.Name.LocalName);

                            //Find the object matching to the data
                            var foundResult = passedResults.Find(
                                ((aMockObject) =>
                                    aMockObject.Name == passedXElement.Value
                                )
                            );

                            //Exclude it from the list of expected results
                            passedResults.Remove(foundResult);

                            return foundResult;
                        }
                    )
                );

            mockFactory
                .SetupGet<string>(
                    ((mockFac) =>
                        mockFac.XmlIdentifier
                    )
                )
                .Returns("mockitem");

            //GENERATION
            var testedService = new LocalFileServiceMultiItem<MockObject>(
                TestData.MOCK_MULTIITEM_DATA,
                "mockitems",
                mockFactory.Object
            );

            //EXECUTION
            var result = testedService.Load();

            //TEST: Each entry in the raw data was passed once to the factory once
            Assert.IsTrue(passedResults.Count == 0, "Some nodes were not passed through the mock factory (and therefore not removed from our list of expected results)");

            //TEST: The factory method was called the expected number of times
            mockFactory
                .Verify<MockObject>(
                    ((mockFac) =>
                        mockFac.Load(
                            It.Is<XElement>(
                                ((xElement) =>
                                    xElement.Name.LocalName == "mockitem"
                                )
                            )
                        )
                    ),
                    Times.Exactly(5),
                    "The factory method was called an unexpected amount of times by the service"
                );            
        }
    }
}
