using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.MainView.Default
{
    internal sealed partial class MainView : RibbonWindow
    {
        public MainView(IMainViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
