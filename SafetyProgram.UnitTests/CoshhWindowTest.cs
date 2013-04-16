using SafetyProgram.MainWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.MainWindow.Commands;
using SafetyProgram.Document;
using SafetyProgram.MainWindow.Ribbons;

namespace SafetyProgram.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CoshhWindowTest and is intended
    ///to contain all CoshhWindowTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CoshhWindowTest
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
        ///A test for CoshhWindow Constructor
        ///</summary>
        [TestMethod()]
        public void CoshhWindowConstructorTest()
        {
            CoshhWindow target = new CoshhWindow();

            Assert.IsNotNull(target.Ribbon, "Ribbon for the CoshhWindow is null");
            Assert.IsNotNull(target.View, "View (window) for the CoshhWindow is null");
            Assert.IsNotNull(target.Commands, "Commands for the CoshhWindow is null");
        }

        /// <summary>
        ///A test for Commands
        ///</summary>
        [TestMethod()]
        public void CommandsTest()
        {
            CoshhWindow target = new CoshhWindow(); // TODO: Initialize to an appropriate value
            WindowCommandsHolder actual;
            actual = target.Commands;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void DocumentTest()
        {
            CoshhWindow target = new CoshhWindow(); // TODO: Initialize to an appropriate value
            CoshhDocument expected = null; // TODO: Initialize to an appropriate value
            CoshhDocument actual;
            target.Document = expected;
            actual = target.Document;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Ribbon
        ///</summary>
        [TestMethod()]
        public void RibbonTest()
        {
            CoshhWindow target = new CoshhWindow(); // TODO: Initialize to an appropriate value
            CoshhRibbon actual;
            actual = target.Ribbon;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [TestMethod()]
        public void ServiceTest()
        {
            CoshhWindow target = new CoshhWindow(); // TODO: Initialize to an appropriate value
            ICoshhDocumentService expected = null; // TODO: Initialize to an appropriate value
            ICoshhDocumentService actual;
            target.Service = expected;
            actual = target.Service;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for View
        ///</summary>
        [TestMethod()]
        public void ViewTest()
        {
            CoshhWindow target = new CoshhWindow(); // TODO: Initialize to an appropriate value
            CoshhWindowView actual;
            actual = target.View;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
