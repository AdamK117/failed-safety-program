using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal static class ConfigHelpers
    {
        public static bool GetDocumentLock(XElement data)
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

        public static string GetLocale(XElement data)
        {
            var element = data.Element("locale");
            if (element != null)
            {
                return element.Value;
            }
            else throw new InvalidDataException("No locale could be found in the configuration file");
        }

        public static IList<IRepositoryInfo> GetRepositories(XElement data)
        {
            //Create a container for the repositories. Can be empty.
            var repositoriesList = new List<IRepositoryInfo>();
            var repositoriesElement = data.Element("repositories");

            if (repositoriesElement != null)
            {
                var repositoriesData = repositoriesElement.Elements("repository");

                foreach (XElement repositoryInfo in repositoriesData)
                {
                    var loadedRepository = GetRepository(repositoryInfo);
                    repositoriesList.Add(loadedRepository);
                }
            }
            return repositoriesList;
        }

        public static IRepositoryInfo GetRepository(XElement data)
        {
            string source, repositoryPath, login, password, repositoryType;

            //Required: Get the source type for the repository
            {
                var sourceAttr = data.Attribute("source");
                if (sourceAttr != null)
                {
                    if (sourceAttr.Value == "local") source = sourceAttr.Value;
                    else if (sourceAttr.Value == "database") source = sourceAttr.Value;
                    else throw new InvalidDataException("Unknown repository source specified.");
                }
                else throw new InvalidDataException("No repository source specified.");
            }

            //Required: Get the path for the repository
            {
                var pathAttr = data.Attribute("path");
                if (pathAttr != null) repositoryPath = pathAttr.Value;
                else throw new InvalidDataException("No path for the repository specified.");
            }

            //Required: Get the content type of the repository
            {
                var typeAttr = data.Attribute("type");
                if (typeAttr != null) repositoryType = typeAttr.Value;
                else throw new InvalidDataException("No type specified for the repository");
            }

            //Optional: Get the username for the repository.
            {
                var loginAttr = data.Attribute("login");
                if (loginAttr != null) login = loginAttr.Value;
                else login = "";
            }

            //Optional: Get the password for the repository (for databases etc.).
            {
                var passwordAttr = data.Attribute("password");
                if (passwordAttr != null) password = passwordAttr.Value;
                else password = "";
            }

            //Compile the data into a repository object
            return new RepositoryInfo(source, repositoryPath, login, password, repositoryType);
        }
    }
}
