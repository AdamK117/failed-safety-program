using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal static class RepositoryDefault
    {
        public static Repository<T> Repository<T>(IFactory<T> entryFactory)
        {
            return new Repository<T>(
                ENTRY_TYPE,
                Entries<T>(),
                entryFactory.CreateNew
                );
        }

        public const string ENTRY_TYPE = "local";

        public static IEnumerable<T> Entries<T>()
        {
            return new List<T>();
        }
    }
}
