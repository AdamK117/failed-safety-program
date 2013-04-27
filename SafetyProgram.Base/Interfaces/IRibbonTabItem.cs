using Fluent;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines a viewable (IViewable) ribbon tab item
    /// </summary>
    public interface IRibbonTabItem : IViewable
    {
        /// <summary>
        /// Gets the RibbonTabItem view for this IRibbonTabItem
        /// </summary>
        new RibbonTabItem View { get; }
    }
}
