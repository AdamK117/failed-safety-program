namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an IRibbonWindow. A window that has a ribbon.
    /// </summary>
    public interface IRibbonWindow : IWindow
    {
        IRibbon Ribbon { get; }
    }
}
