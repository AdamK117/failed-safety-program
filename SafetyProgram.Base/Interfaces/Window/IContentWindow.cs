using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines a content window. A window that holds an IViewable for content.
    /// </summary>
    public interface IContentWindow : IWindow
    {
        IViewable Content { get; }
    }
}
