using System;
using System.Collections.Generic;

namespace SafetyProgram.Configuration
{
    public interface ICallbackService<TContent>
    {
        IEnumerable<TContent> LoadContent();
        IEnumerable<TContent> LoadContent(Action<TContent> callback);        
    }
}
