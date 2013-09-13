using System.Windows.Controls;

namespace SafetyProgram.UI.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableView : UserControl
    {
        public ChemicalTableView(IChemicalTableViewModel viewModel)
        {
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
