using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IConfiguration : IStorable<IConfiguration>
    {
        bool DocumentLock { get; }
        IList<IRepositoryInfo> RepositoriesInfo { get; }
        string Locale { get; }
    }
}
