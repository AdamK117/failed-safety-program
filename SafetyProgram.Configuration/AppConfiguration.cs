using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration
{
    internal sealed class AppConfiguration : IConfiguration
    {
        public AppConfiguration()
        {
            DocumentLock = false;
            Repositories = new List<IRepositoryInfo>();
            Locale = "en-GB";
        }

        public AppConfiguration(bool documentLock, IList<IRepositoryInfo> repositories, string locale)
        {
            this.DocumentLock = documentLock;
            this.Repositories = repositories;
            this.Locale = locale;
        }

        public bool DocumentLock
        {
            get;
            private set;
        }

        public IList<IRepositoryInfo> Repositories
        {
            get;
            private set;
        }

        public string Locale
        {
            get;
            private set;
        }

        public IConfiguration LoadFromXml(XElement data)
        {
            bool documentLock = ConfigHelpers.GetDocumentLock(data);
            IList<IRepositoryInfo> loadedRepositories = ConfigHelpers.GetRepositories(data);
            string loadedLocale = ConfigHelpers.GetLocale(data);

            return new AppConfiguration(documentLock, loadedRepositories, loadedLocale);
        }

        public XElement WriteToXElement()
        {
            return
                new XElement(Identifier,
                    new XElement("documentlock", DocumentLock ? "true" : "false"),
                    new XElement("repositories", 
                        from repositoryInfo in Repositories
                        select repositoryInfo.WriteToXElement()
                    ),
                    new XElement("locale", Locale)
                );
        }

        public string Identifier
        {
            get { return XmlNodeNames.AppConfig; }
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
