using System.IO;

namespace SafetyProgram.Static
{
    /// <summary>
    /// Acts as a centralized location for paths to test data et. al. Unit tests using I/O should try and centralize their absolute paths here.
    /// </summary>
    public static class TestData
    {
        /// <summary>
        /// Folder containing all relevant test data
        /// </summary>
        public const string TestFolder = "V:\\SafetyProgram\\SafetyProgram.TestData";

        /// <summary>
        /// File containing correct configuration file data
        /// </summary>
        public static readonly string ConfigFile = Path.Combine(TestFolder, "ConfigurationFile.xml");

        /// <summary>
        /// File containing delibarately invalid data
        /// </summary>
        public static readonly string InvalidConfigFile = Path.Combine(TestFolder, "InvalidConfigurationFile.xml");
    }
}
