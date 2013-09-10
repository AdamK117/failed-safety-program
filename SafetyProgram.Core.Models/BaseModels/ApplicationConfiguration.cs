using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SafetyProgram.Core.Models
{
    public sealed class ApplicationConfiguration : 
        IApplicationConfiguration
    {
        public bool DocumentLock
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

        public string Identifier
        {
            get { return ModelIdentifiers.APPLICATION_CONFIGURATION_IDENTIFIER; }
        }

        public ObservableCollection<IRepositoryInfo> Content
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<IApplicationModel> IHasMany.Content
        {
            get { throw new NotImplementedException(); }
        }
    }
}
