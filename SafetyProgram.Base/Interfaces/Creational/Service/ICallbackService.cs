using System;
using System.Collections.Generic;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines a service that loads IEnumerable's of items individually.
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public interface IServiceMultiItem<TContent> : IOutputService<IEnumerable<TContent>>
    {
        IEnumerable<TContent> Load(Action<TContent> callback);
    }
}
