using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public class RepositoryLocalFileFactory<T> : ILocalFileFactory<IRepository<T>>
    {
        private readonly ILocalFileFactory<T> entryFactory;

        public RepositoryLocalFileFactory(ILocalFileFactory<T> entryFactory)
        {
            if (entryFactory != null) this.entryFactory = entryFactory;
            else throw new ArgumentNullException();
        }

        public static IRepository<T> StaticCreateNew(IFactory<T> entryFactory)
        {
            return RepositoryDefault.Repository(entryFactory);
        }
        public IRepository<T> CreateNew()
        {
            return StaticCreateNew(entryFactory);
        }

        public static IRepository<T> StaticLoad(ILocalFileFactory<T> entryFactory, XElement data)
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

            return new Repository<T>(
                loadedEntryType,
                loadedEntries,
                loadedEntryConstructor
                );
        }
        public IRepository<T> Load(XElement data)
        {
            return StaticLoad(entryFactory, data);
        }

        public static XElement StaticStore(ILocalFileFactory<T> entryFactory, IRepository<T> item)
        {
            //TODO: Validation check
            return
                new XElement(XML_IDENTIFIER,
                    new XAttribute("type", item.EntryType),
                    from entry in item.Entries
                    select entryFactory.Store(entry)
                );
        }
        public XElement Store(IRepository<T> item)
        {
            return StaticStore(entryFactory, item);
        }

        public const string XML_IDENTIFIER = "repository";
        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
