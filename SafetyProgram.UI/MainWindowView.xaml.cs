using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.MainWindowUi
{
    public sealed partial class MainWindowView : RibbonWindow
    {
        public MainWindowView(IMainWindowViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Hotkeys);
        }
    }
}
