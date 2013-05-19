using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal static class AppConfigurationDefault
    {
        public static AppConfiguration AppConfiguration()
        {
            return new AppConfiguration(
                DocumentLock,
                RepositoryInfo(),
                LOCALE,
                LOCAL_CONNECTION_TYPE
                );
        }

        public const bool DocumentLock = false;

        public static IEnumerable<IRepositoryInfo> RepositoryInfo()
        {
            return new List<IRepositoryInfo>();
        }

        public const string LOCALE = "en-GB";
        public const string LOCAL_CONNECTION_TYPE = "local";
        public const string DATABASE_CONNECTION_TYPE = "database";
    }
}
