using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal sealed class AppConfiguration : IConfiguration
    {
        public AppConfiguration(
            bool documentLock, 
            IEnumerable<IRepositoryInfo> repositories, 
            string locale,
            string connectionType
            )
        {
            this.DocumentLock = documentLock;

            if (repositories != null)
            {
                this.repositories = repositories;
            }
            else throw new ArgumentNullException("The repositories passed to AppConfiguration must not be null.");

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

        public string Error
        {
            get { throw new System.NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
