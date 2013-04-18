using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafetyProgram.Document;
using SafetyProgram.Document.Services;
using SafetyProgram.BaseClasses.DocumentFormats;

namespace SafetyProgram.UnitTests
{
    [TestClass]
    public class CoshhDocumentApiTests
    {
        Mock<ICoshhDocumentService> serviceMock;
        Mock<CoshhDocument> documentMock;

        public CoshhDocumentApiTests()
        {
            serviceMock = new Mock<ICoshhDocumentService>();
            documentMock = new Mock<CoshhDocument>();
        }

        /// <summary>
        /// Checks that the CoshhWindows Document can be changed (via its setter) and that, when it is changed, it correctly triggers the DocumentChanged event.
        /// </summary>
        [TestMethod]
        public void CoshhWindow_ChangeDocument()
        {
            CoshhWindow window = new CoshhWindow(serviceMock.Object, documentMock.Object);

            List<ICoshhDocument> eventResponse  = new List<ICoshhDocument>();
            
            window.DocumentChanged += (document) =>
            {
                eventResponse.Add(document);
            };

            ICoshhDocument newDocument = new CoshhDocument(new A4DocFormat());

            window.Document = newDocument;

            Assert.AreNotEqual(0, eventResponse.Count, "The CoshhDocument.DocumentChanged event did not trigger");
            Assert.AreEqual(1, eventResponse.Count, "The CoshhDocument.DocumentChanged event triggered more than once when the CoshhDocument was only set once");
            Assert.AreEqual(eventResponse[0], newDocument, "The CoshhDocument.DocumentChanged event didn't send the newly set CoshhDocument.");
        }

        /// <summary>
        /// Tests that CoshhWindow.Document may be set to null and that, when done so, it triggers the CoshhWindow.DocumentChanged event with a null reference
        /// </summary>
        [TestMethod]
        public void CoshhWindow_SetDocumentNull()
        {
            CoshhWindow window = new CoshhWindow(serviceMock.Object, documentMock.Object);

            ICoshhDocument eventResponse = new CoshhDocument(new A4DocFormat());

            window.DocumentChanged += (document) =>
                {
                    eventResponse = document;
                };

            window.Document = null;

            Assert.IsNull(eventResponse, "CoshhDocument.DocumentChanged event did not send a null reference when CoshhWindow.Document was set to null (it should)");
        }

        /// <summary>
        /// Tests that the service may be changed (via its setter) and that the CoshhWindow.ServiceChanged event correctly triggers
        /// </summary>
        [TestMethod]
        public void CoshhWindow_ChangeService()
        {
            CoshhWindow window = new CoshhWindow(serviceMock.Object, documentMock.Object);

            List<ICoshhDocumentService> eventResponse = new List<ICoshhDocumentService>();

            window.ServiceChanged += (service) =>
                {
                    eventResponse.Add(service);
                };

            Mock<ICoshhDocumentService> newService = new Mock<ICoshhDocumentService>();

            window.ChangeService(newService.Object);

            Assert.AreEqual(1, eventResponse.Count, "The CoshhDocument.ServiceChanged didn't trigger / triggered more than once!");
            Assert.AreEqual(eventResponse[0], newService.Object, "The CoshhDocument.ServiceChanged event didn't send the newly set ICoshhDocumentService (it's different)");
        }

        /// <summary>
        /// Tests that the CoshhWindow's service cannot be set to null. There must be a valid ICoshhDocumentService set for the CoshhWindow
        /// </summary>
        [TestMethod]
        public void CoshhWindow_SetServiceToNull()
        {
            CoshhWindow window = new CoshhWindow(serviceMock.Object, documentMock.Object);

            object eventResponse = null;

            window.ServiceChanged += (service) =>
                {
                    eventResponse = service;
                };

            try
            {
                window.ChangeService(null);
            }
            catch(ArgumentNullException)
            {
                //The proper exception was thrown
            }

            Assert.IsNull(eventResponse, "The CoshhWindow.ServiceChanged event triggered when the CoshhWindow.Service was set to null (it shouldn't)");
        }

        /// <summary>
        /// Ensures the CoshhWindow constructs properly and exposes a predictable API.
        /// </summary>
        [TestMethod]
        public void CoshhWindow_TestConstruction()
        {
            Mock<ICoshhDocumentService> mockService = new Mock<ICoshhDocumentService>();
            Mock<CoshhDocument> mockDocument = new Mock<CoshhDocument>();

            CoshhWindow window = new CoshhWindow(mockService.Object, mockDocument.Object);

            Assert.AreSame(window.Service, mockService.Object, "CoshhWindow.Service is not the same as the ICoshhDocumentService provided in its constructor");
            Assert.AreSame(window.Document, mockDocument.Object, "CoshhWindow.Document is not the same as the CoshhDocument provided in its constructor");
            Assert.IsNotNull(window.Commands, "CoshhWindow.Commands have not been properly initialized");
            Assert.IsNotNull(window.Ribbon, "CoshhWindow.Ribbon has not been properly initialized");
            Assert.IsNotNull(window.View, "CoshhWindow.View has not been properly initialized");
        }
    }
}
