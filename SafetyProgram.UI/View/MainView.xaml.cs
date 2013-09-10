using System.Windows;
using System.Windows.Input;
using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI
{
    public sealed partial class MainView : RibbonWindow
    {
        public MainView(IMainViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Hotkeys);
        }

        private void Handle(object sender, MouseButtonEventArgs e)
        {
            var origSource = e.OriginalSource;
        }
    }
}
