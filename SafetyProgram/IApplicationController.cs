using SafetyProgram.MainWindowUi;
using SafetyProgram.Models;

namespace SafetyProgram
{
    /// <summary>
    /// Defines an interface for the controller of the application.
    /// </summary>
    public interface IApplicationController
    {
        /// <summary>
        /// Get the commands available to the application.
        /// </summary>
        IApplicationCommands Commands { get; }

        /// <summary>
        /// Get the controller for the main window.
        /// </summary>
        IMainWindowController WindowController { get; }

        /// <summary>
        /// Get the application configuration.
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
