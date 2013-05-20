using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.Configuration.UnitTests
{
    [TestClass]
    public class LocalConfigurationFileTest
    {
        private XElement getTestData()
        {
            return XElement.Load(TestData.CONFIGURATION_FILE);
        }

        private XElement getInvalidTestData()
        {
            return XElement.Load(TestData.INVALID_CONFIGURATION_FILE);
        }

        private ILocalFileFactory<IConfiguration> getFactory()
        {
            return new ConfigurationLocalFileFactory(new RepositoryInfoLocalFileFactory(), new ChemicalModelObjectLocalFileFactory());
        }

        [TestMethod]
        public void LoadFileTest()
        {
            //Tests that the LocalConfigurationFile correctly loads an Xml format configuration from the designated path.
            //Expected behaviour:
            //  All values are loaded without throwing an exception
            //  Repository is serialized correctly. It's a local file repository so username/password should be blank

            //SETUP: Get test data
            var data = getTestData();

            var factory = getFactory();

            //EXECUTE: Load the config file data using the factory
            var loadedConfig = factory.Load(data);

            Assert.AreEqual<bool>(loadedConfig.DocumentLock, false);
            Assert.AreEqual<string>(loadedConfig.Locale, "en-GB");

            //First repository entry in the test data: local, C:\repository.xml.
            var enumerator = loadedConfig.RepositoriesInfo.GetEnumerator();
            enumerator.MoveNext();
            var testRepos = enumerator.Current;

            Assert.AreEqual<string>(testRepos.Source, "local", "An incorrect source type was serialized into the repository. Should be 'local'.");
            Assert.AreEqual<string>(testRepos.Path, "V:\\SafetyProgram\\SafetyProgram.TestData\\ChemicalRepository.xml", "An incorrect path was serialized into the repository when compared with the test data");
            Assert.AreEqual<string>(testRepos.Login, "", "A login was serialized into the repository object. Local files (e.g. in the test data) shouldn't have a login");
            Assert.AreEqual<string>(testRepos.Password, "", "A password was serialized into the repository object. Local files (e.g. in the test data) shouldn't have a password");

            //Second repository entry in the test data: database, \\myserver\sqlAddr, Admin, password
            enumerator.MoveNext();
            var testDbRepos = enumerator.Current;

            Assert.AreEqual<string>(testDbRepos.Source, "database");
            Assert.AreEqual<string>(testDbRepos.Path, "\\\\myserver\\sqlAddr");
            Assert.AreEqual<string>(testDbRepos.Login, "Admin");
            Assert.AreEqual<string>(testDbRepos.Password, "password");
        }

        [TestMethod]
        public void InvalidDataTest()
        {   
            //  Expected Behaviours:
            //      -1) The invalid data should throw up a InvalidDataException
            //          Test: Check for an InvalidDataException


            //SETUP: Get invalid test data
            var invalidData = getInvalidTestData();

            var factory = getFactory();

            try
            {
                //EXECUTE: Load the invalid data
                factory.Load(invalidData);
                Assert.Fail("Invalid data should produce a System.IO.InvalidDataException, this invalid data does not");
            }
            catch (InvalidDataException)
            {
                //The right exception, test passed.
            }
        }
    }
}
