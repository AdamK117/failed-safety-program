using System.Windows.Controls;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a UI controller (mvC).
    /// </summary>
    public interface IUiController
    {
        /// <summary>
        /// Get the view the controller oversees (mVc).
        /// </summary>
        Control View { get; }
    }
}
