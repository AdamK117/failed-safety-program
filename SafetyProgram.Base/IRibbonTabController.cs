using Fluent;

namespace SafetyProgram.Base
{
    public interface IRibbonTabController :
        IUiController
    {
        new RibbonTabItem View { get; }
    }
}
