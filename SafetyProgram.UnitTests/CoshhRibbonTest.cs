using SafetyProgram.Ribbons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SafetyProgram;
using SafetyProgram.Document;
using SafetyProgram.Document.Commands;
using SafetyProgram.Commands;
using Moq;
using SafetyProgram.Document.Services;
using SafetyProgram.BaseClasses.DocumentFormats;

namespace SafetyProgram.UnitTests
{  
    /// <summary>
    ///This is a test class for CoshhRibbonTest and is intended
    ///to contain all CoshhRibbonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoshhRibbonTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CoshhRibbon Constructor
        ///</summary>
        [TestMethod()]
        public void CoshhRibbonConstructorTest()
        {
            ////Mock a CoshhWindow and its Commands
            //Mock<CoshhWindow> mockWindow = new Mock<CoshhWindow>();
            //Mock<WindowCommandsHolder> mockWindowCommands = new Mock<WindowCommandsHolder>();
            //mockWindow.SetupGet(window => window.Commands).Returns(mockWindowCommands.Object);  

            ////Mock a CoshhDocument and its Commands
            //Mock<CoshhDocument> mockDocument = new Mock<CoshhDocument>();
            //Mock<DocumentCommandsHolder> mockDocumentCommands = new Mock<DocumentCommandsHolder>();
            //mockDocument.SetupGet(document => document.Commands).Returns(mockDocumentCommands.Object);

            ////Add the mocked CoshhDocument to the mocked CoshhWindow
            //mockWindow.SetupGet(window => window.Document).Returns(mockDocument.Object);

            //CoshhRibbon target = new CoshhRibbon(mockWindow.Object);

            //target.

            //Assert.AreSame(target.WindowCommands, mockWindowCommands);
            //Assert.AreSame(target.DocumentCommands, mockDocumentCommands.Object);
        }

        /// <summary>
        ///A test for documentChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SafetyProgram.exe")]
        public void documentChangedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            CoshhRibbon_Accessor target = new CoshhRibbon_Accessor(param0); // TODO: Initialize to an appropriate value
            CoshhDocument document = null; // TODO: Initialize to an appropriate value
            target.documentChanged(document);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for DocumentCommands
        ///</summary>
        [TestMethod()]
        public void DocumentCommandsTest()
        {
            CoshhWindow window = null; // TODO: Initialize to an appropriate value
            CoshhRibbon target = new CoshhRibbon(window); // TODO: Initialize to an appropriate value
            DocumentCommandsHolder actual;
            actual = target.DocumentCommands;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RibbonVisibility
        ///</summary>
        [TestMethod()]
        public void RibbonVisibilityTest()
        {
            CoshhWindow window = null; // TODO: Initialize to an appropriate value
            CoshhRibbon target = new CoshhRibbon(window); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RibbonVisibility;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for View
        ///</summary>
        [TestMethod()]
        public void ViewTest()
        {
            Mock<ICoshhDocumentService> mockService = new Mock<ICoshhDocumentService>();

            CoshhDocument mockDocument = new CoshhDocument(new A4DocFormat());

            CoshhWindow window = new CoshhWindow(mockService.Object, mockDocument);
            CoshhRibbon target = new CoshhRibbon(window);

            Assert.IsNotNull(target.View);
        }

        /// <summary>
        ///A test for WindowCommands
        ///</summary>
        [TestMethod()]
        public void WindowCommandsTest()
        {
            CoshhWindow window = null; // TODO: Initialize to an appropriate value
            CoshhRibbon target = new CoshhRibbon(window); // TODO: Initialize to an appropriate value
            WindowCommandsHolder actual;
            actual = target.WindowCommands;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
