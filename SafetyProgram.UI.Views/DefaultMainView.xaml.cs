using Fluent;
using SafetyProgram.Base.FSharp;

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
