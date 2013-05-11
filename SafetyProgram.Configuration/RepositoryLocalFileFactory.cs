using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.Configuration
{
    public class RepositoryLocalFileFactory<T> : ILocalFileFactory<IRepository<T>>
    {
        private readonly ILocalFileFactory<T> entryFactory;

        public RepositoryLocalFileFactory(ILocalFileFactory<T> entryFactory)
        {
            this.entryFactory = entryFactory;
        }

        public IRepository<T> CreateNew()
        {
            return new Repository<T>("local", new List<T>(), entryFactory.CreateNew);
        }

        public IRepository<T> Load(XElement data)
        {
            string loadedEntryType = entryFactory.XmlIdentifier;
            var loadedEntries = new List<T>();
            Func<T> loadedEntryConstructor = entryFactory.CreateNew;

            var entryElements = data.Elements(entryFactory.XmlIdentifier);
            foreach (XElement entryElement in entryElements)
            {
                var newEntry = entryFactory.Load(entryElement);
                loadedEntries.Add(newEntry);
            }

            Debug.Assert(entryElements.Count() > 0, "Empty repository found");

            return new Repository<T>(loadedEntryType, loadedEntries, loadedEntryConstructor);
        }

        public XElement Store(IRepository<T> item)
        {
            return
                new XElement(XML_IDENTIFIER,
                    new XAttribute("type", item.EntryType),
                    from entry in item.Entries
                    select entryFactory.Store(entry)
                );
        }

        public const string XML_IDENTIFIER = "repository";
        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
