using System.Collections.Generic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines a standard implementation for the applications configuration.
    /// </summary>
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Get the document lock state for the configuration. The document
        /// lock should prevent the execution of some commands.
        /// </summary>
        bool DocumentLock { get; }

        /// <summary>
        /// Get the repositories available to the document.
        /// </summary>
        IEnumerable<IRepositoryInfo> RepositoriesInfo { get; }

        /// <summary>
        /// Get the locale of the application (en-GB, fr-CA, etc.).
        /// </summary>
        string Locale { get; }

        /// <summary>
        /// Get the connection type used by the application (local files, remote, etc.).
        /// </summary>
        string ConnectionType { get; }
    }
}
