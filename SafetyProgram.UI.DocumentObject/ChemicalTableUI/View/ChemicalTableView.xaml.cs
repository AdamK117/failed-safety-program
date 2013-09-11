using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;

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
