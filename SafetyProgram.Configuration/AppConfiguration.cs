using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Static;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal sealed class AppConfiguration : IConfiguration
    {
        public AppConfiguration()
        {
            DocumentLock = false;
            this.repositories = new List<IRepositoryInfo>();
            Locale = "en-GB";
        }

        public AppConfiguration(bool documentLock, IList<IRepositoryInfo> repositories, string locale)
        {
            this.DocumentLock = documentLock;

            if (repositories != null)
            {
                this.repositories = repositories;
            }
            else throw new ArgumentNullException("The repositories IList passed to AppConfiguration must not be null.");

            this.Locale = locale;
        }

        public bool DocumentLock
        {
            get;
            private set;
        }

        private readonly IList<IRepositoryInfo> repositories;
        public IList<IRepositoryInfo> RepositoriesInfo
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

        public static IConfiguration ConstructFromXml(XElement data)
        {
            bool documentLock = ConfigHelpers.GetDocumentLock(data);
            IList<IRepositoryInfo> loadedRepositories = ConfigHelpers.GetRepositories(data);
            string loadedLocale = ConfigHelpers.GetLocale(data);

            return new AppConfiguration(documentLock, loadedRepositories, loadedLocale);
        }

        public IConfiguration LoadFromXml(XElement data)
        {
            return ConstructFromXml(data);
        }

        public XElement WriteToXElement()
        {
            //TODO: Error check
            return
                new XElement(Identifier,
                    new XElement("documentlock", DocumentLock ? "true" : "false"),
                    new XElement("repositories", 
                        from repositoryInfo in RepositoriesInfo
                        select repositoryInfo.WriteToXElement()
                    ),
                    new XElement("locale", Locale)
                );
        }

        public string Identifier
        {
            get { return XmlNodeNames.APP_CONFIG; }
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
