using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration.UnitTests
{
    [TestClass]
    public sealed class RepositoryCallbackServiceTest
    {
        private ICallbackService<IChemicalModelObject> getCallbackService()
        {
            return new LocalRepositoryService<IChemicalModelObject>(
                TestData.CHEMICAL_REPOSITORY,
                new ChemicalModelObjectLocalFileFactory()
                );
        }

        [TestMethod]
        public void LoadContentTest()
        {
            var service = getCallbackService();

            var loadedChemicals = service.LoadContent();
        }

        [TestMethod]
        public void LoadContentCallbackTest()
        {
            var service = getCallbackService();

            int callbackCalls = 0;
            Action<IChemicalModelObject> callback =
                (retrievedData) =>
                {
                    callbackCalls++;
                };

            //EXECUTE
            service.LoadContent(callback);

            Assert.IsTrue(callbackCalls > 0);
        }
    }
}
