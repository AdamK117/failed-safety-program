using Fluent;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a controller for a ribbon view.
    /// </summary>
    public interface IRibbonViewController : 
        IUiController
    {
        /// <summary>
        /// Get the ribbon view this controller oversees.
        /// </summary>
        new Ribbon View { get; }
    }
}
