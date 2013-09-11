using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;

namespace SafetyProgram.Base
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
