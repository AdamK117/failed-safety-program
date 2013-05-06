using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;
using System.Diagnostics;

namespace SafetyProgram.Configuration
{
    public class Repository<T> : IRepository<T>
        where T : IStorable<T>
    {
        public Repository(string entryType, IEnumerable<T> entries, Func<T> entryConstructor)
        {
            EntryType = entryType;
            Entries = entries;
            EntryConstructor = entryConstructor;
        }

        public Repository(Func<T> ctor)
        {
            EntryConstructor = ctor;
        }

        public Func<T> EntryConstructor
        {
            get;
            private set;
        }

        public string EntryType
        {
            get;
            private set;
        }

        public IEnumerable<T> Entries
        {
            get;
            private set;
        }

        public IRepository<T> LoadFromXml(XElement data)
        {
            string loadedEntryType;
            List<T> loadedEntries = new List<T>();
            Func<T> loadedEntryConstructor;

            loadedEntryConstructor = () => Activator.CreateInstance<T>();

            var referenceEntry = loadedEntryConstructor();

            var entryElements = data.Elements(referenceEntry.Identifier);         
            foreach (XElement entryElement in entryElements)
            {
                var newEntry = EntryConstructor();
                newEntry.LoadFromXml(entryElement);
                loadedEntries.Add(newEntry);
            }
            Debug.Assert(entryElements.Count() > 0, "Empty repository found");

            loadedEntryType = referenceEntry.Identifier;

            return new Repository<T>(loadedEntryType, loadedEntries, loadedEntryConstructor);
        }

        public XElement WriteToXElement()
        {
            return
                new XElement(Identifier,
                    new XAttribute("type", EntryType),
                    from entry in Entries
                    select entry.WriteToXElement()
                );
        }

        public string Identifier
        {
            get { return XmlNodeNames.Repository; }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
