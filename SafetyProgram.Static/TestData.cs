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
        public const string TEST_FOLDER = "V:\\SafetyProgram\\SafetyProgram.TestData";

        /// <summary>
        /// File containing correct configuration file data
        /// </summary>
        public static readonly string CONFIGURATION_FILE = Path.Combine(TEST_FOLDER, "ConfigurationFile.xml");

        /// <summary>
        /// File containing delibarately invalid data
        /// </summary>
        public static readonly string INVALID_CONFIGURATION_FILE = Path.Combine(TEST_FOLDER, "InvalidConfigurationFile.xml");

        /// <summary>
        /// A standard Coshh file
        /// </summary>
        public static readonly string COSHH_FILE = Path.Combine(TEST_FOLDER, "CoshhFile.xml");

        public static readonly string CHEMICAL_REPOSITORY = Path.Combine(TEST_FOLDER, "ChemicalRepository.xml");
    }
}
