using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IConfiguration
    {
        bool DocumentLock { get; }
        IEnumerable<IRepositoryInfo> RepositoriesInfo { get; }
        string Locale { get; }
    }
}
