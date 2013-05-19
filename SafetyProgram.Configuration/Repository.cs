using System;
using System.Collections.Generic;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public sealed class Repository<TContent> : 
        IRepository<TContent>
    {
        public Repository(
            string entryType, 
            IEnumerable<TContent> entries, 
            Func<TContent> entryConstructor)
        {
            EntryType = entryType;

            if (entries != null && entryConstructor != null)
            {
                Entries = entries;
                EntryConstructor = entryConstructor;
            }
            else throw new ArgumentNullException();
        }

        public Func<TContent> EntryConstructor
        {
            get;
            private set;
        }

        public string EntryType
        {
            get;
            private set;
        }

        public IEnumerable<TContent> Entries
        {
            get;
            private set;
        }

        IList<string> validationErrorList = new List<string>();

        public string Error
        {
            get 
            {
                return ErrorValidation.JoinErrors(validationErrorList);
            }
        }

        public string this[string columnName]
        {
            get 
            {
                validationErrorList.Clear();
                //No error checks for this (yet)
                return null;
            }
        }
    }
}
