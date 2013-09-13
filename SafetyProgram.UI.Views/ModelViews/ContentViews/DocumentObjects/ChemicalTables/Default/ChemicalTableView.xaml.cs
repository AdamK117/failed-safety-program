using System.Windows.Controls;

namespace SafetyProgram.UI.Views.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public sealed partial class ChemicalTableView : UserControl
    {
        public ChemicalTableView(IChemicalTableViewModel viewModel)
        {
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
