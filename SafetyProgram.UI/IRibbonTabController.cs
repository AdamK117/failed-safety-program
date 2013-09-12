using Fluent;

namespace SafetyProgram.UI
{
    public interface IRibbonTabController :
        IUiController
    {
        new RibbonTabItem View { get; }
    }
}
