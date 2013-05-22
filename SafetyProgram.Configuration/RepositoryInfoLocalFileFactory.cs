using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class RepositoryInfoLocalFileFactory 
        : ILocalFileFactory<IRepositoryInfo>
    {
        public static IRepositoryInfo StaticCreateNew()
        {
            return RepositoryInfoDefault.RepositoryInfo();
        }

        public IRepositoryInfo CreateNew()
        {
            return StaticCreateNew();
        }

        public static IRepositoryInfo StaticLoad(XElement data)
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

        public IRepositoryInfo Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(IRepositoryInfo item)
        {
            return
                new XElement(XML_IDENTIFIER,
                    new XAttribute("source", item.Source),
                    new XAttribute("type", item.ContentType),
                    new XAttribute("path", item.Path),
                    new XAttribute("login", item.Login),
                    new XAttribute("password", item.Password)
                );
        }

        public XElement Store(IRepositoryInfo item)
        {
            return StaticStore(item);
        }

        public const string XML_IDENTIFIER = "repositoryinfo";
        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
