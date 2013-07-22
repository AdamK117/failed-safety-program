using System.Windows.Input;

namespace SafetyProgram
{
    /// <summary>
    /// Defines an interface for a holder of the applications commands.
    /// </summary>
    interface IApplicationCommands
    {
        /// <summary>
        /// Get a command that exits the application.
        /// </summary>
        ICommand Exit { get; }
    }
}
