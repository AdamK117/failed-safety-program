using System;
using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IRepository<TContent>
    {
        Func<TContent> EntryConstructor { get; }
        string EntryType { get; }
        IEnumerable<TContent> Entries { get; }
    }
}
