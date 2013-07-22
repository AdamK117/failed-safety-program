using System.Windows;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentUi;
using SafetyProgram.MainWindowUi.Commands;
using SafetyProgram.Models;

namespace SafetyProgram.MainWindowUi
{
    /// <summary>
    /// Defines an interface for the main window controller.
    /// </summary>
    public interface IMainWindowController : IViewable
    {
        /// <summary>
        /// Get the service used by the window.
        /// </summary>
        IIOService<IDocument> Service { get; }

        /// <summary>
        /// Get the commands available to the window.
        /// </summary>
        IWindowCommands Commands { get; }

        /// <summary>
        /// Get the document controller open in the window.
        /// </summary>
        IDocumentController Document { get; }

        /// <summary>
        /// Get the window control.
        /// </summary>
        new Window View { get; }
    }
}
