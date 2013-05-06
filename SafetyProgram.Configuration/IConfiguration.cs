using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public interface IConfiguration : IStorable
    {
        bool DocumentLock { get; }
        IList<IRepositoryInfo> Repositories { get; }
        string Locale { get; }
    }
}
