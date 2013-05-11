using Fluent;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableRibbonView : RibbonTabItem
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
