using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration
{
    internal sealed class RepositoryInfo : IRepositoryInfo
    {
        public RepositoryInfo()
        { }

        public RepositoryInfo(string type, string path, string login, string password, string contentType)
        {
            Source = type;
            Path = path;
            Login = login;
            Password = password;
            ContentType = contentType;
        }

        public string Source
        {
            get;
            private set;
        }

        public string Path
        {
            get;
            private set;
        }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public string ContentType
        {
            get;
            private set;
        }

        public static IRepositoryInfo ConstructFromXml(XElement data)
        {
            return ConfigHelpers.GetRepository(data);
        }

        public IRepositoryInfo LoadFromXml(XElement data)
        {
            return LoadFromXml(data);
        }

        public XElement WriteToXElement()
        {
            //TODO: Error check
            return
                new XElement(Identifier,
                    new XAttribute("source", Source),
                    new XAttribute("type", ContentType),
                    new XAttribute("path", Path),
                    new XAttribute("login", Login),
                    new XAttribute("password", Password)
                );
        }

        public string Identifier
        {
            get { return XmlNodeNames.REPOSITORY_INFO; }
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
