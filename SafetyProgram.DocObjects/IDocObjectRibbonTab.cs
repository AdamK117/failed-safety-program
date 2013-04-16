using Fluent;

namespace SafetyProgram.DocObjects
{
    public interface IDocObjectRibbonTab
    {
        /// <summary>
        /// Gets the RibbonTabItem view for the DocObjectRibbon
        /// </summary>
        RibbonTabItem View { get; }
    }
}
