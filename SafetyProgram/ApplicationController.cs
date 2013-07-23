using System.Windows;
using SafetyProgram.MainWindowUi;
using SafetyProgram.MainWindowUi.View;
using SafetyProgram.Models;

namespace SafetyProgram
{
    /// <summary>
    /// Provides an implementation of the IApplicationController interface.
    /// </summary>
    public sealed class ApplicationController
    {
        public ApplicationController(IApplicationKernel applicationKernel)
        {
            // Get the configuration file
            IConfiguration configuration = null;

            Window window = new MainWindowView(
                new MainWindowViewModel(
                    applicationKernel.Window,
                    configuration
                )
            );

            window.Show();
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
        public Models.IConfiguration Configuration
        {
            get { return configuration; }
        }
    }
}
