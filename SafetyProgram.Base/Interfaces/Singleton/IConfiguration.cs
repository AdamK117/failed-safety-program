using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IConfiguration
    {
        bool DocumentLock { get; }
        IEnumerable<IRepositoryInfo> RepositoriesInfo { get; }

        IEnumerable<IRepository<IChemicalModelObject>> ChemicalRepositories { get; }

        string Locale { get; }
        string ConnectionType { get; }
    }
}
