using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Core.Models;
using Moq;
using SafetyProgram.Core.Commands.KernelCommands;
using System.Collections.Generic;

namespace SafetyProgram.Core.Commands.UnitTests
{
    [TestClass]
    public class SelectionHelpersTests
    {
        [TestMethod]
        public void GetModelTest()
        {
            var mockTable1 = new Mock<IChemicalTable>();
            var mockTable2 = new Mock<IChemicalTable>();
            var mockFormat = new Mock<IFormat>();
            var mockDocument = new Document(
                new System.Collections.ObjectModel.ObservableCollection<IDocumentObject>()
                {
                    mockTable1.Object,
                    mockTable2.Object
                },
                mockFormat.Object);

            var retrievedSelection = SelectionHelpers.GetSelection(
                new List<int>() { 0 },
                mockDocument);

            Assert.AreEqual(retrievedSelection, mockTable1.Object);
        }
    }
}
