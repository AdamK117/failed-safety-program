using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.Views.MainView.Default
{
    public sealed partial class MainViews : RibbonWindow
    {
        public MainViews(IMainViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
