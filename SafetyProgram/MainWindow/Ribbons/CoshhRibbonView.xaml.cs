using Fluent;

namespace SafetyProgram.MainWindow.Ribbons
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    public partial class CoshhRibbonView : Ribbon
    {
        private readonly CoshhRibbon viewModel;

        public CoshhRibbonView(CoshhRibbon viewModel)
        {
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
