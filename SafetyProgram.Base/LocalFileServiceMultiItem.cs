using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class LocalFileServiceMultiItem<TContent> : IServiceMultiItem<TContent>
    {
        bool canLoad = true;

        private readonly string path;
        private readonly string rootNodeName;
        private readonly ILocalFileFactory<TContent> contentFactory;        

        public LocalFileServiceMultiItem(string path, 
            string rootNodeName, 
            ILocalFileFactory<TContent> contentFactory)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            else this.path = path;

            this.rootNodeName = rootNodeName;

            if (contentFactory == null)
            {
                throw new ArgumentNullException();
            }
            else this.contentFactory = contentFactory;
        }

        public IEnumerable<TContent> Load()
        {
            //Non callback (traditional IService) loading. 
            //Use a blank callback with the load method.
            return Load((content) => { });
        }

        /// <summary>
        /// Loads elements from an XML source, putting them through the callback as each is retrieved.
        /// </summary>
        /// <param name="callback">Callback that is called whenever an element is loaded.</param>
        /// <returns>All loaded elements</returns>
        public IEnumerable<TContent> Load(Action<TContent> callback)
        {
            if (CanLoad())
            {
                //Define a return value container.
                var loadedEntries = new List<TContent>();

                //Load the XML file.
                var sourceXml = XDocument.Load(path);

                //Required: Get the root node (e.g. 'repositories', 'chemicals').
                var root = sourceXml.Element(rootNodeName);
                if (root != null)
                {
                    //Optional (it may be empty): Get the elements contained within the root using the supplied factory.
                    var entries = root.Elements(contentFactory.XmlIdentifier);

                    foreach (XElement entry in entries)
                    {
                        var loadedEntry = contentFactory.Load(entry);
                        callback(loadedEntry);
                        loadedEntries.Add(loadedEntry);
                    }
                }
                else throw new InvalidDataException();

                return loadedEntries;
            }
            else throw new InvalidOperationException();
        }

        public bool CanLoad()
        {
            return canLoad;
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
