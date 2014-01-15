using SafetyProgram.Base;
using SafetyProgram.UI.Models;
using SafetyProgram.UI.ViewModels;
using System.Windows.Controls;

namespace SafetyProgram.UI.Views
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public sealed partial class DefaultChemicalTableView : UserControl
    {
        private readonly IChemicalTableViewModel viewModel;

        public DefaultChemicalTableView(IChemicalTableViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }

        private void Chemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (GuiCoshhChemical chemical in e.AddedItems)
            {
                chemical.Selected = true;
                viewModel.SelectedChemicals.Add(chemical);
            }

            foreach (GuiCoshhChemical chemical in e.RemovedItems)
            {
                chemical.Selected = false;
                viewModel.SelectedChemicals.Remove(chemical);
            }
        }
    }
}
