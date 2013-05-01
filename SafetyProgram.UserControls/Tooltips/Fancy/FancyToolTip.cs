using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.UserControls.Tooltips.Fancy
{
    public sealed class FancyToolTip : IToolTip
    {
        private readonly FancyToolTipView view;

        public FancyToolTip(string header, string icon, string body)
        {
            Header = header;
            Icon = icon;
            Body = body;

            view = new FancyToolTipView(this);
        }

        public string Header { get; private set; }
        public string Icon { get; private set; }        
        public string Body { get; private set; }

        public ToolTip View
        {
            get { return view; }
        }

        Control IViewable.View
        {
            get { return view; }
        }
    }
}
