using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class LocalRepositoryService<TContent> : ICallbackService<TContent>
    {
        private readonly ILocalFileFactory<TContent> contentFactory;
        private readonly string path;

        public LocalRepositoryService(string path, 
            ILocalFileFactory<TContent> contentFactory)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            else this.path = path;

            if (contentFactory == null)
            {
                throw new ArgumentNullException();
            }
            else this.contentFactory = contentFactory;
        }

        public IEnumerable<TContent> LoadContent()
        {
            return LoadContent((content) => { });
        }

        public IEnumerable<TContent> LoadContent(Action<TContent> callback)
        {
            var loadedEntries = new List<TContent>();

            var file = XDocument.Load(path);
            var root = file.Element("repository");
            var entries = root.Elements(contentFactory.XmlIdentifier);            

            foreach (XElement entry in entries)
            {
                var loadedEntry = contentFactory.Load(entry);
                callback(loadedEntry);
                loadedEntries.Add(loadedEntry);
            }

            return loadedEntries;
        }
    }
}
