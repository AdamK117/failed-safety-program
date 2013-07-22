using System.Collections.Generic;

namespace SafetyProgram.Models
{
    public interface IConfiguration
    {
        bool DocumentLock { get; }
        IEnumerable<IRepositoryInfo> RepositoriesInfo { get; }

        IEnumerable<IRepository<IChemical>> ChemicalRepositories { get; }

        string Locale { get; }
        string ConnectionType { get; }
    }
}
