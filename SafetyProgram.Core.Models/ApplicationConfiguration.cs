using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Core.Models
{
    public sealed class ApplicationConfiguration : 
        IApplicationConfiguration
    {
        public bool DocumentLock
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IRepositoryInfo> RepositoriesInfo
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
