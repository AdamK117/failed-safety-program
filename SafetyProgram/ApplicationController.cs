using System.Windows;
using SafetyProgram.Core;
using SafetyProgram.UI;

namespace SafetyProgram
{
    /// <summary>
    /// Provides an implementation of the IApplicationController interface.
    /// </summary>
    public sealed class ApplicationController
    {
        public ApplicationController(IApplicationKernel applicationKernel)
        {

        }

        private readonly IApplicationCommands commands;

        /// <summary>
        /// Get the commands available to the application.
        /// </summary>
        public IApplicationCommands Commands
        {
            get { return commands; }
        }

        private IConfiguration configuration;

        /// <summary>
        /// Get the configuration for the application.
        /// </summary>
        public IConfiguration Configuration
        {
            get { return configuration; }
        }
    }
}
