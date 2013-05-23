using Fluent;

namespace MockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : RibbonWindow
    {
        public MainWindowView(IMainWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
