using SafetyProgram.Base;
using SafetyProgram.UI.ViewModels;
using System.Windows.Controls;

namespace SafetyProgram.UI.Views
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public sealed partial class DefaultChemicalTableView : UserControl
    {
        public DefaultChemicalTableView(IChemicalTableViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
