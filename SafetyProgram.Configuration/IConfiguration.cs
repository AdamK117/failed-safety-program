using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public interface IConfiguration : IStorable<IConfiguration>
    {
        bool DocumentLock { get; }
        IList<IRepositoryInfo> Repositories { get; }
        string Locale { get; }
    }
}
