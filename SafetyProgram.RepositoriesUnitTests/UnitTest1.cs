using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Repositories;

namespace SafetyProgram.RepositoriesUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LocalFileRepository<string> a = new LocalFileRepository<string>("yo");
        }
    }
}
