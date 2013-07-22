using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.MainWindowUi
{
    public sealed partial class CoshhWindowView : RibbonWindow
    {
        public CoshhWindowView(ICoshhWindowViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Hotkeys);
        }
    }
}
