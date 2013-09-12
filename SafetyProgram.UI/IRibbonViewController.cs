using Fluent;

namespace SafetyProgram.UI
{
    public interface IRibbonViewController : 
        IUiController
    {
        new Ribbon View { get; }
    }
}
