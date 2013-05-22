using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal static class AppConfigurationDefault
    {
        public static AppConfiguration AppConfiguration()
        {
            return new AppConfiguration(
                DEFAULT_DOCUMENT_LOCK,
                DefaultRepositoryInfo(),
                DefaultChemicalRepositories(),
                DEFAULT_LOCALE,
                DEFAULT_CONNECTION_TYPE_LOCAL
                );
        }

        public const bool DEFAULT_DOCUMENT_LOCK = false;

        public static IEnumerable<IRepositoryInfo> DefaultRepositoryInfo()
        {
            return new List<IRepositoryInfo>();
        }

        public static IEnumerable<IRepository<IChemicalModelObject>> DefaultChemicalRepositories()
        {
            return new List<IRepository<IChemicalModelObject>>();
        }

        public const string DEFAULT_LOCALE = "en-GB";
        public const string DEFAULT_CONNECTION_TYPE_LOCAL = "local";
        public const string DEFAULT_CONNECTION_TYPE_DATABASE = "database";
    }
}
