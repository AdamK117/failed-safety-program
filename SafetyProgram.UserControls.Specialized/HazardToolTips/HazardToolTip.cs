using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;
using SafetyProgram.UserControls.Tooltips;
using SafetyProgram.UserControls.Tooltips.Fancy;

namespace SafetyProgram.UserControls.Specialized.HazardToolTips
{
    public class HazardToolTip : IToolTip
    {
        private readonly FancyToolTip view;

        public HazardToolTip(IHazardModelObject model)
        {
            view = new FancyToolTip(model.Hazard, model.Symbol, model.SignalWord);
        }

        public System.Windows.Controls.ToolTip View
        {
            get { return view.View; }
        }

        System.Windows.Controls.Control Base.Interfaces.IViewable.View
        {
            get { return view.View; }
        }
    }
}
