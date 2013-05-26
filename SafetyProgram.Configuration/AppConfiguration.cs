using System.Collections.Generic;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal sealed class AppConfiguration : IConfiguration
    {
        public AppConfiguration(bool documentLock, 
            IEnumerable<IRepositoryInfo> repositoriesInfo, 
            IEnumerable<IRepository<IChemicalModelObject>> chemicalRepository,
            string locale,
            string connectionType)
        {
            Helpers.NullCheck(repositoriesInfo, chemicalRepository);

            this.DocumentLock = documentLock;
            this.repositories = repositoriesInfo;
            this.chemicalRepositories = chemicalRepository;
            this.ConnectionType = connectionType;
            this.Locale = locale;
        }

        public bool DocumentLock
        {
            get;
            private set;
        }

        private readonly IEnumerable<IRepositoryInfo> repositories;
        public IEnumerable<IRepositoryInfo> RepositoriesInfo
        {
            get
            {
                return repositories;
            }
        }

        private readonly IEnumerable<IRepository<IChemicalModelObject>> chemicalRepositories;
        public IEnumerable<IRepository<IChemicalModelObject>> ChemicalRepositories
        {
            get
            {
                return chemicalRepositories;
            }
        }

        public string Locale
        {
            get;
            private set;
        }

        public string ConnectionType
        {
            get;
            private set;
        }

        private IList<string> validationErrorList = new List<string>();
        public string Error
        {
            get 
            {
                return ErrorValidation.JoinErrors(validationErrorList);
            }
        }

        public string this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    case "ConnectionType":
                        if (
                            (ConnectionType != AppConfigurationDefault.DEFAULT_CONNECTION_TYPE_LOCAL) &&
                            (ConnectionType != AppConfigurationDefault.DEFAULT_CONNECTION_TYPE_DATABASE)
                            )
                        {
                            const string ERROR_CONNECTION_TYPE_UNKNOWN = "Connection type for the app_config is unknown. Known examples: 'local' & 'database";

                            return ERROR_CONNECTION_TYPE_UNKNOWN;
                        }
                        break;                        
                }
                return null;
            }
        }        
    }
}
