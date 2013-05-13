using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class Repository<T> : 
        IRepository<T>
    {
        public Repository(
            string entryType, 
            IEnumerable<T> entries, 
            Func<T> entryConstructor)
        {
            EntryType = entryType;

            if (entries != null && entryConstructor != null)
            {
                Entries = entries;
                EntryConstructor = entryConstructor;
            }
            else throw new ArgumentNullException();
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
