using System.Windows.Controls;

namespace SafetyProgram.UI.Views.ModelViews.ChemicalTableViews
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public sealed partial class DefaultChemicalTableView : UserControl
    {
        public DefaultChemicalTableView(IChemicalTableViewModel viewModel)
        {
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
