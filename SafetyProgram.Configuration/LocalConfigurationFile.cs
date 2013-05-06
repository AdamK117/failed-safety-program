using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class LocalConfigurationFile : IService<IConfiguration>
    {
        private readonly string path;

        public LocalConfigurationFile(string path)
        {
            if (File.Exists(path)) this.path = path;
            else throw new FileNotFoundException("Specified configuration file path (" + path + ") could not be found.");
        }

        public IConfiguration New()
        {
            return new AppConfiguration();
        }

        public bool CanNew()
        {
            return true;
        }

        /// <summary>
        /// Load a configuration file locally (xml format).
        /// </summary>
        /// <returns>The loaded configuration file</returns>
        /// <exception cref="System.IO.InvalidDataException"></exception>
        public IConfiguration Load()
        {
            var configFile = XDocument.Load(path);

            var configuration = configFile.Element("appconfig");
            if (configuration != null)
            {
                var config = new AppConfiguration();
                config.LoadData(configuration);
                return config;
            }
            else throw new InvalidDataException("No appconfig root found in the specified configuration file");
        }

        public bool CanLoad()
        {
            return true;
        }

        public void Save(IConfiguration data)
        {
            throw new NotImplementedException();
        }

        public bool CanSave(IConfiguration data)
        {
            throw new NotImplementedException();
        }

        public void SaveAs(IConfiguration data)
        {
            throw new NotImplementedException();
        }

        public bool CanSaveAs(IConfiguration data)
        {
            throw new NotImplementedException();
        }

        public void Close(IConfiguration data)
        {
            throw new NotImplementedException();
        }
    }
}
