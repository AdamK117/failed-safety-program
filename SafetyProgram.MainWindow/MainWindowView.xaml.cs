using System.Windows.Controls;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
