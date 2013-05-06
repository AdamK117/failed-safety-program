using System.Xml.Linq;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration
{
    internal sealed class RepositoryInfo : IRepositoryInfo
    {
        public RepositoryInfo()
        { }

        public RepositoryInfo(IRepositoryInfo reposInfo)
        {
            Source = reposInfo.Source;
            Path = reposInfo.Path;
            Login = reposInfo.Login;
            Password = reposInfo.Password;
            ContentType = reposInfo.ContentType;
        }

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

        public void LoadData(XElement data)
        {
            var loadedRepository = ConfigHelpers.GetRepository(data);
            Source = loadedRepository.Source;
            Path = loadedRepository.Path;
            Login = loadedRepository.Login;
            Password = loadedRepository.Password;
            ContentType = loadedRepository.ContentType;
        }

        public XElement WriteToXElement()
        {
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
            get { return XmlNodeNames.RepositoryInfo; }
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
