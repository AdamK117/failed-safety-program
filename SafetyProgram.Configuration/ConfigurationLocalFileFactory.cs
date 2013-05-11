using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public class ConfigurationLocalFileFactory 
        : ILocalFileFactory<IConfiguration>
    {
        public static IConfiguration StaticCreateNew()
        {
            return new AppConfiguration();
        }
        public IConfiguration CreateNew()
        {
            return StaticCreateNew();
        }

        private static bool getDocumentLock(XElement data)
        {
            var element = data.Element("documentlock");

            if (element != null)
            {
                var docLock = element.Value;
                if (docLock == "false") return false;
                else if (docLock == "true") return true;
                else throw new InvalidDataException("The configuration value for documentLock was invalid. It must be true or false.");
            }
            else throw new InvalidDataException("The documentlock value could not be found in the configuration file");
        }
        private static string getLocale(XElement data)
        {
            var element = data.Element("locale");
            if (element != null)
            {
                return element.Value;
            }
            else throw new InvalidDataException("No locale could be found in the configuration file");
        }
        private static IEnumerable<IRepositoryInfo> getRepositories(XElement data)
        {
            var repositoriesElement = data.Element("repositories");
            if (repositoriesElement != null)
            {
                var repositoriesData = repositoriesElement.Elements(RepositoryInfoLocalFileFactory.XML_IDENTIFIER);
                foreach (XElement repositoryInfo in repositoriesData)
                {
                    yield return RepositoryInfoLocalFileFactory.StaticLoad(repositoryInfo);
                }
            }
        }
        public static IConfiguration StaticLoad(XElement data)
        {
            bool documentLock = getDocumentLock(data);
            IEnumerable<IRepositoryInfo> loadedRepositories = getRepositories(data);
            string loadedLocale = getLocale(data);

            return new AppConfiguration(documentLock, loadedRepositories, loadedLocale);
        }
        public IConfiguration Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(IConfiguration item)
        {
            //TODO: Error check
            return
                new XElement(XML_IDENTIFIER,
                    new XElement("documentlock", item.DocumentLock ? "true" : "false"),
                    new XElement("repositories",
                        from repositoryInfo in item.RepositoriesInfo
                        select RepositoryInfoLocalFileFactory.StaticStore(repositoryInfo)
                    ),
                    new XElement("locale", item.Locale)
                );
        }
        public XElement Store(IConfiguration item)
        {
            return StaticStore(item);
        }

        public const string XML_IDENTIFIER = "appconfig";
        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
