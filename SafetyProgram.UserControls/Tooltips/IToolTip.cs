using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.UserControls.Tooltips
{
    public interface IToolTip : IViewable
    {
        new ToolTip View { get; }
    }
}
