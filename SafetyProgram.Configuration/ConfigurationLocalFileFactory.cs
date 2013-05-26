using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public class ConfigurationLocalFileFactory 
        : ILocalFileFactory<IConfiguration>
    {
        private readonly ILocalFileFactory<IRepositoryInfo> repositoryInfoFactory;
        private readonly ILocalFileFactory<IChemicalModelObject> chemicalLocalFileFactory;

        public ConfigurationLocalFileFactory(ILocalFileFactory<IRepositoryInfo> repositoryInfoFactory,
            ILocalFileFactory<IChemicalModelObject> chemicalLocalFileFactory)
        {
            Helpers.NullCheck(repositoryInfoFactory, chemicalLocalFileFactory);

            this.repositoryInfoFactory = repositoryInfoFactory;
            this.chemicalLocalFileFactory = chemicalLocalFileFactory;
        }

        public IConfiguration CreateNew()
        {
            return AppConfigurationDefault.AppConfiguration();
        }

        private const string DOCUMENT_XML_IDENTIFIER = "documentlock";
        private static bool getDocumentLock(XElement data)
        {
            var element = data.Element(DOCUMENT_XML_IDENTIFIER);

            if (element != null)
            {
                var docLock = element.Value;
                if (docLock == "false") return false;
                else if (docLock == "true") return true;
                else throw new InvalidDataException("The configuration value for documentLock was invalid. It must be true or false.");
            }
            else throw new InvalidDataException("The documentlock value could not be found in the configuration file");
        }

        private const string LOCALE_XML_IDENTIFIER = "locale";
        private static string getLocale(XElement data)
        {
            return
                data
                .Element(LOCALE_XML_IDENTIFIER)
                .ExtractStrict("No locale could be found in the configuration file");
        }

        private const string REPOSITORY_INFO_XML_IDENTIFIER = "repositories";
        private IEnumerable<IRepositoryInfo> getRepositoriesInfo(XElement data)
        {
            var repositoriesElement = data.Element(REPOSITORY_INFO_XML_IDENTIFIER);
            if (repositoriesElement != null)
            {
                var repositoriesData = repositoriesElement.Elements(repositoryInfoFactory.XmlIdentifier);
                foreach (XElement repositoryInfo in repositoriesData)
                {
                    yield return repositoryInfoFactory.Load(repositoryInfo);
                }
            }
        }

        private IEnumerable<IRepository<IChemicalModelObject>> getChemicalRepositories(IEnumerable<IRepositoryInfo> repositoryInfo)
        {
            var repositories = new List<IRepository<IChemicalModelObject>>();

            foreach (IRepositoryInfo info in repositoryInfo)
            {
                if (info.Source == AppConfigurationDefault.DEFAULT_CONNECTION_TYPE_LOCAL)
                {
                    if (info.ContentType == chemicalLocalFileFactory.XmlIdentifier)
                    {
                        var repository = new Repository<IChemicalModelObject>(
                            new LocalFileServiceMultiItem<IChemicalModelObject>(
                                info.Path,
                                "repository",
                                chemicalLocalFileFactory
                            )
                        );
                        repositories.Add(repository);
                    }
                }
            }

            return repositories;
        }

        public IConfiguration Load(XElement data)
        {
            var loadedRepositories = getRepositoriesInfo(data);
            var loadedChemicalRepositoies = getChemicalRepositories(loadedRepositories);

            return new AppConfiguration(
                getDocumentLock(data),
                loadedRepositories,
                loadedChemicalRepositoies,
                getLocale(data),
                AppConfigurationDefault.DEFAULT_CONNECTION_TYPE_LOCAL);
        }

        public static XElement StaticStore(IConfiguration item)
        {
            //TODO: Validation check
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
