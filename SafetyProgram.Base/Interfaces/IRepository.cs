using System;
using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IRepository<T>
    {
        Func<T> EntryConstructor { get; }
        string EntryType { get; }
        IEnumerable<T> Entries { get; }
    }
}
