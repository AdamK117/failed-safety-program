using System;
using SafetyProgram.Base;
using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.Core
{
    /// <summary>
    /// Defines an application kernel. The overall structure of the SafetyProgram application.
    /// </summary>
    public interface IApplicationKernel
    {
        /// <summary>
        /// Get or set the document of the application (may be null).
        /// </summary>
        IDocument Document { get; set; }

        /// <summary>
        /// Occurs when the applications document changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IDocument>> DocumentChanged;

        /// <summary>
        /// Get or set the IO service used by the application.
        /// </summary>
        IIOService<IDocument> Service { get; set; }

        /// <summary>
        /// Occurs when the service changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IIOService<IDocument>>> ServiceChanged;

        /// <summary>
        /// Get the configuration used by the application.
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
