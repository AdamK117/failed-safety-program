using System;
using SafetyProgram.Base;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines an interface for a window model.
    /// </summary>
    public interface IWindowKernel
    {
        /// <summary>
        /// Get the document in the window.
        /// </summary>
        IDocument Document { get; }

        /// <summary>
        /// Occurs when the document changes.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<IDocument>> DocumentChanged;
    }
}
