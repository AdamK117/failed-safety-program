using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration
{
    public interface IRepository<T> : IStorable
        where T : IStorable
    {
        Func<T> EntryConstructor { get; }
        string EntryType { get; }
        IEnumerable<T> Entries { get; }
    }
}
