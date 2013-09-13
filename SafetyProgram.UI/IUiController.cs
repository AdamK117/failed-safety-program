using System.Windows.Controls;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a generic controller.
    /// </summary>
    public interface IUiController
    {
        /// <summary>
        /// Get the generic control view the controller oversees.
        /// </summary>
        Control View { get; }
    }
}
