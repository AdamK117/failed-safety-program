using System.Windows.Input;

namespace SafetyProgram.Core.Commands
{
    /// <summary>
    /// Defines the commands available to the window.
    /// </summary>
    public interface ICoreCommands
    {    
        /// <summary>
        /// Get a command for creating a new document within the window.
        /// </summary>
        ICommand New { get; }

        /// <summary>
        /// Get a command for opening a document in the window.
        /// </summary>
        ICommand Open { get; }

        /// <summary>
        /// Get a command for saving document in the window.
        /// </summary>
        ICommand Save { get; }

        /// <summary>
        /// Get a command for saving the current document to a user-specified location
        /// </summary>
        ICommand SaveAs { get; }

        /// <summary>
        /// Get a command that closes the current document.
        /// </summary>
        ICommand Close { get; }

        /// <summary>
        /// Get a command that performs an undo operation on the current document.
        /// </summary>
        ICommand Undo { get; }

        /// <summary>
        /// Get a command that performs a redo operation on the current document.
        /// </summary>
        ICommand Redo { get; }
    }
}
