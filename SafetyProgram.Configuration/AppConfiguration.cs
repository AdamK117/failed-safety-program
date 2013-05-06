using System.Collections.Generic;
using SafetyProgram.Static;
using System.Xml.Linq;
using System.Linq;
using System.IO;

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

        public void LoadData(XElement data)
        {
            DocumentLock = ConfigHelpers.GetDocumentLock(data);
            Locale = ConfigHelpers.GetLocale(data);
            Repositories = ConfigHelpers.GetRepositories(data);
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
