using System.Windows;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a controller for a window view.
    /// </summary>
    public interface IWindowController : 
        IUiController
    {
        /// <summary>
        /// Get the window this controller oversees.
        /// </summary>
        new Window View { get; }
    }
}
