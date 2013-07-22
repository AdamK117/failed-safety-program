using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    internal sealed class RepositoryInfo : 
        IRepositoryInfo
    {
        public RepositoryInfo(string type, 
            string path, 
            string login, 
            string password, 
            string contentType)
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
