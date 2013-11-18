using Fluent;
using SafetyProgram.Base.CSharp;

namespace SafetyProgram.UI.Views.MainViews
{
    public sealed partial class DefaultMainView : RibbonWindow
    {
        public DefaultMainView(IMainViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
