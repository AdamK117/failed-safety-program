using System;
using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    public interface IRepository<TContent>
    {
        IEnumerable<TContent> Get(Func<TContent, bool> comparison, Action<TContent> callback);
        IEnumerable<TContent> GetAll(Action<TContent> callback);
        IEnumerable<TContent> GetAll();
    }
}
