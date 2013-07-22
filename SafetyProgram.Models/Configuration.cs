using System;
using System.Collections.Generic;

namespace SafetyProgram.Models
{
    public sealed class Configuration : IConfiguration
    {
        public bool DocumentLock
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRepositoryInfo> RepositoriesInfo
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRepository<IChemical>> ChemicalRepositories
        {
            get { throw new NotImplementedException(); }
        }

        public string Locale
        {
            get { throw new NotImplementedException(); }
        }

        public string ConnectionType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
