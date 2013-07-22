using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines an interface for the application model.
    /// </summary>
    public interface IApplicationKernel
    {
        /// <summary>
        /// Get the window model used in the application.
        /// </summary>
        IWindowKernel Window { get; }

        /// <summary>
        /// Occurs when the window changes.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<IWindowKernel>> WindowChanged;
    }
}
