using System;
using System.Windows.Controls;
using SafetyProgram.UserControls.Tooltips;

namespace SafetyProgram.UserControls.TagList
{
    public sealed class TagListItem : ITagListItem
    {
        public string Label { get; private set; }
        public IToolTip ToolTip { get; private set; }
        public Action Remove { get; private set; }

        /// <summary>
        /// Creates a new Tag List Item
        /// </summary>
        /// <param name="label">The label shown for the tag</param>
        /// <param name="remove">The action to perform when the X button is clicked</param>
        public TagListItem(string label, Action remove)
        {
            Label = label;
            Remove = remove;
        }

        public TagListItem(string label, Action remove, IToolTip toolTip)
            : this(label, remove)
        {
            ToolTip = toolTip;            
        }
    }
}
