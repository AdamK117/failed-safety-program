using Fluent;
using SafetyProgram.Base;
using SafetyProgram.UI.ViewModels;

namespace SafetyProgram.UI.Views
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
