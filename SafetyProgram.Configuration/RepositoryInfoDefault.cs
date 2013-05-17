namespace SafetyProgram.Configuration
{
    internal static class RepositoryInfoDefault
    {
        public static RepositoryInfo RepositoryInfo()
        {
            return new RepositoryInfo(
                TYPE,
                PATH,
                LOGIN,
                PASSWORD,
                CONTENT_TYPE
                );
        }

        public const string TYPE = "";
        public const string PATH = "";
        public const string LOGIN = "";
        public const string PASSWORD = "";
        public const string CONTENT_TYPE = "";
    }
}
