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
        where T : IStorable
    {
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

        public void LoadData(XElement data)
        {
            var loadedEntries = new List<T>();
            var referenceEntry = EntryConstructor();

            var entryElements = data.Elements(referenceEntry.Identifier);

            Debug.Assert(entryElements.Count() > 0, "Empty repository found");
           
            foreach (XElement entryElement in entryElements)
            {
                var newEntry = EntryConstructor();
                newEntry.LoadData(entryElement);
                loadedEntries.Add(newEntry);
            }

            Entries = loadedEntries;
            EntryType = referenceEntry.Identifier;
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
