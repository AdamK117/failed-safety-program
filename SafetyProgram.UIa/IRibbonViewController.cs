using Fluent;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a controller fro a ribbon view.
    /// </summary>
    public interface IRibbonViewController : 
        IUiController
    {
        /// <summary>
        /// Get the view this controller oversees.
        /// </summary>
        new Ribbon View { get; }
    }
}
