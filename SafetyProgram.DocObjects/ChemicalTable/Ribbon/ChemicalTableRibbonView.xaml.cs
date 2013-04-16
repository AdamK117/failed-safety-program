using Fluent;

namespace SafetyProgram.DocObjects.ChemicalTable.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    public partial class ChemicalTableRibbonView : RibbonTabItem
    {
        private readonly ChemicalTableRibbonTab viewModel;

        public ChemicalTableRibbonView(ChemicalTableRibbonTab viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
