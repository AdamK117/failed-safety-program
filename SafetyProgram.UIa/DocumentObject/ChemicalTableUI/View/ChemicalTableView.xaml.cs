using System.Windows.Controls;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableView : UserControl
    {
        private readonly IChemicalTableViewModel viewModel;

        public ChemicalTableView(IChemicalTableViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
