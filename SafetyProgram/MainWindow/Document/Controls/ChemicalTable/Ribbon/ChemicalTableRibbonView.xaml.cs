using Fluent;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    public partial class ChemicalTableRibbonView : RibbonTabItem
    {
        private readonly ChemicalTableRibbon viewModel;

        public ChemicalTableRibbonView(ChemicalTableRibbon viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
