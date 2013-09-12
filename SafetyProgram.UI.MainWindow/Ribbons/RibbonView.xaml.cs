using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.MainWindow
{
    internal sealed partial class RibbonView : Ribbon
    {
        private readonly IRibbonViewModel viewModel;

        public RibbonView(IRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
