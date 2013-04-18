using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Repositories;
using SafetyProgram.Repositories.UnitTests;

namespace SafetyProgram.Repositories.UnitTests
{
    [TestClass]
    public class LocalFileModelRepositoryTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            LocalFileModelRepository a = new LocalFileModelRepository("hi");
        }
    }
}
