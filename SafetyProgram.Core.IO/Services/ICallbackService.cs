using System;
using System.Collections.Generic;

namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines a service that loads IEnumerable's of items individually. Calling a callback for each item.
    /// </summary>
    /// <typeparam name="TContent">The type of content to be loaded through the service. 
    /// The content will be handled in an IEnumerable for callback.</typeparam>
    public interface IServiceMultiItem<TContent> : 
        IOutputService<IEnumerable<TContent>>
    {
        /// <summary>
        /// Load data through the service. Calling a callback each time an item is loaded.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IEnumerable<TContent> Load(Action<TContent> callback);
    }
}
