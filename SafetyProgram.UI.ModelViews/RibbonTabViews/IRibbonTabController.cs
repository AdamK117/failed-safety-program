using Fluent;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a controller for a ribbon tab view.
    /// </summary>
    public interface IRibbonTabController :
        IUiController
    {
        /// <summary>
        /// Get the ribbon tab view this controller oversees.
        /// </summary>
        new RibbonTabItem View { get; }
    }
}
