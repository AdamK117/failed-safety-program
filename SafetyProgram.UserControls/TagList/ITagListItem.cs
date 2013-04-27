using System;
using System.Windows.Controls;
using SafetyProgram.UserControls.Tooltips;

namespace SafetyProgram.UserControls.TagList
{
    public interface ITagListItem
    {
        string Label { get; }
        IToolTip ToolTip { get; }
        Action Remove { get; }
    }
}
