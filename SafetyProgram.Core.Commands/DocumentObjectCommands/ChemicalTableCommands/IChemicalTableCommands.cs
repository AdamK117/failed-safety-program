using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Core.Commands
{
    /// <summary>
    /// Defines an interface for the commands available to the chemical table.
    /// </summary>
    public interface IChemicalTableCommands : ICommandsHolder
    {
        /// <summary>
        /// Get a command that deletes the currently selected item(s) in the chemical table.
        /// </summary>
        ICommand DeleteSelected { get; }

        /// <summary>
        /// Get a command that copys the currently selected chemical(s) in the chemical table.
        /// </summary>
        ICommand CopySelected { get; }

        /// <summary>
        /// Get a command that pastes chemicals into the chemical table from the clipboard.
        /// </summary>
        ICommand PasteChemicals { get; }

        /// <summary>
        /// Get a commmand that inserts a chemical into the chemical table.
        /// </summary>
        ICommand InsertChemical { get; }
    }
}
