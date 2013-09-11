using System.Windows.Controls;
using SafetyProgram.Base;

namespace SafetyProgram.UI.ContentViews
{
    /// <summary>
    /// Interaction logic for ContentView.xaml
    /// </summary>
    public partial class ContentView : UserControl
    {
        private readonly IContentViewModel viewModel;

        public ContentView(IContentViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
