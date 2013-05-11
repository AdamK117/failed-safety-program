using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration.UnitTests
{
    [TestClass]
    public class LocalConfigurationFileTest
    {
        [TestMethod]
        public void LoadFileTest()
        {
            //Tests that the LocalConfigurationFile correctly loads an Xml format configuration from the designated path.
            //Expected behaviour:
            //  All values are loaded without throwing an exception
            //  Repository is serialized correctly. It's a local file repository so username/password should be blank

            var configService = new ConfigurationLocalFileService(TestData.CONFIGURATION_FILE);

            var configFile = configService.Load();

            Assert.AreEqual<bool>(configFile.DocumentLock, false);
            Assert.AreEqual<string>(configFile.Locale, "en-GB");

            //First repository entry in the test data: local, C:\repository.xml.
            var enumerator = configFile.RepositoriesInfo.GetEnumerator();
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
        public void InvalidPathTest()
        {
            //Tests that the Local file service throws an exception early
            //  Make an instance of it with an invalid path
            //  Should immediately throw (not when you call a method)

            try
            {
                var configService = new ConfigurationLocalFileService("SomeFakePath");
                Assert.Fail("The service should throw a FileNotFound exception if constructed with an incorrect path");
            }
            catch (FileNotFoundException)
            {
                //The right exception, test passed.
            }            
        }

        [TestMethod]
        public void InvalidDataTest()
        {
            //Loads a known invalid file
            //  Should throw an System.IO.InvalidDataException

            var configService = new ConfigurationLocalFileService(TestData.INVALID_CONFIGURATION_FILE);

            try
            {
                configService.Load();
                Assert.Fail("Invalid data should produce a System.IO.InvalidDataException, this invalid data does not");
            }
            catch (InvalidDataException)
            {
                //The right exception, test passed.
            }
        }
    }
}
