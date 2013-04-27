using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.UserControls.Tooltips
{
    public sealed class DefaultToolTip : IToolTip
    {
        private readonly ToolTip view;

        public DefaultToolTip(string header)
        {
            Header = header;
            view = new DefaultToolTipView(this);
        }

        public string Header { get; private set; }

        public ToolTip View { get { return view; } }

        Control IViewable.View { get { return view; } }
    }
}
